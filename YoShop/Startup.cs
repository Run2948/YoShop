using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
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
                //                .UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText)) //监听SQL命令对象，在执行前
                .UseAutoSyncStructure(true) //自动同步实体结构【开发环境必备】
                .UseNoneCommandParameter(true) //不使用命令参数化执行，针对 Insert/Update
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
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                    p.AllowCredentials();
                });
            });
            //注入HttpClient
            services.AddHttpClient();
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
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //解决razor视图中中文被编码的问题
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            }
            //配置跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.AllowCredentials();
            });
            //            启动Response缓存
            app.UseResponseCaching();

            //配置默认路由
            app.UseMvcWithDefaultRoute();
            //            app.UseMvc(routes =>
            //            {
            //                routes.MapRoute(
            //                    name: "default",
            //                    template: "{controller=Home}/{action=Index}/{id?}");
            //            });
        }
    }
}
