using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Masuit.Tools.AspNetCore.Mime;
using Masuit.Tools.Core.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.WeChat;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace YoShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, Configuration.GetConnectionString("Mysql"))
                //.UseLazyLoading(false) //开启延时加载功能，相当于执行了 1+N 次数据库查询，比较适合【 WinForm 开发】像树形结构，可以点击再展开UI，使用 Include(a => a.UploadFile) 只会查一次数据库
                //由于null会默认输出日志到控制台，影响测试结果。这里传入一个空的日志输出对象
                .UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText)) //监听SQL命令对象，在执行前
                .UseAutoSyncStructure(true) //自动同步实体结构【开发环境必备】
                .UseNoneCommandParameter(true) //不使用命令参数化执行，针对 Insert/Update 方便调试时直接展示无参SQL
                .Build();
            fsql.Aop.ConfigEntityProperty += (_, e) =>
            {
                if (fsql.Ado.DataType == FreeSql.DataType.MySql || fsql.Ado.DataType == FreeSql.DataType.OdbcMySql) return;
                if (e.Property.PropertyType.IsEnum == false) return;
                e.ModifyResult.MapType = typeof(string);
            };
            fsql.Aop.CurdBefore += (_, e) => Trace.WriteLine(e.Sql);
            // 实现全局控制租户
            fsql.GlobalFilter.Apply<WxappEntity>("Wxapp", a => a.WxappId == GlobalConfig.TalentId);
            Fsql = fsql;
        }

        public IFreeSql Fsql { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //读取应用程序配置
            Configuration.GetSectionValue<AppConfig>();
            //配置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆
                    // 注意，http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                    policy
                        // 不支持向所有域名开放， AllowAnyOrigin 不可用
                        //                        .WithOrigins(AppSettings.app(new string[] { "Startup", "Cors", "IPs" }).Split(','))
                        .AllowAnyHeader()//允许任何头
                        .AllowAnyMethod()//允许任何方式
                        .AllowCredentials();//允许cookie
                });
            });
            /** .NET Core 中正确使用 HttpClient 的姿势
             * https://www.cnblogs.com/willick/p/net-core-httpclient.html
             */
            // 注入HttpClient
            services.AddHttpClient();
            services.AddHttpClient<WxHttpClient>();
            /** .NET Core 中正确使用 MemoryCache 的姿势
             * https://www.cnblogs.com/gygg/p/11275417.html
             */
            services.AddMemoryCache(options =>
            {
                // 设置缓存压缩比为 2%
                options.CompactionPercentage = 0.02D;
                //每 5 分钟进行一次过期缓存的扫描
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
                // 最大缓存空间大小限制为 1024
                // options.SizeLimit = 1024;
            });
            //注入静态HttpContext
            services.AddHttpContextAccessor();
            // HttpContextAccessor 默认实现了IHttpContextAccessor接口，它简化了HttpContext的操作
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //注入响应缓存
            services.AddResponseCaching();
            //配置请求长度
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;// 100MB
            });
            //配置请求压缩
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<CustomCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "image/svg+xml"
                });
            });
            //注入Session
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromHours(2);
                opt.Cookie.HttpOnly = true;
            });
            services.AddSingleton(Fsql);
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvcCore().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //解决razor视图中中文被编码的问题
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //                app.UseHsts();
            }
            //配置跨域
            app.UseCors("LimitRequests");
            // 启动Response缓存
            app.UseResponseCaching();
            app.UseResponseCompression();
            // URL重写
            app.UseRewriter(new RewriteOptions().AddRedirectToNonWww());
            //注入静态HttpContext对象
            app.UseStaticHttpContext();
            //注入Session
            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions //静态资源缓存策略
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,no-cache";
                    ctx.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.AddDays(7).ToString("R");
                },
                ContentTypeProvider = new FileExtensionContentTypeProvider(MimeMapper.MimeTypes)
            });
            app.UseCookiePolicy();
            //启用网站防火墙
            app.UseRequestIntercept();
            if (AppConfig.IsDebug)
            {
                //初始化系统设置参数
                GlobalConfig.TalentId = 10001;
                var settings = Fsql.Select<Setting>().ToList();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
                //                var wxapp = Fsql.Select<Wxapp>().Where(l => l.WxappId == GlobalConfig.TalentId).ToOneAsync();
                //                GlobalConfig.WxappConfig = wxapp.Mapper<WxappConfig>();
            }
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            app.UseRouting();
            //配置路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
