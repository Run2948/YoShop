using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YoShop.Extensions.Common;
using YoShop.Models;

namespace YoShop.Extensions
{
    /// <summary>
    /// 时间转换扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static uint ConvertToTimeStamp(this DateTime dt)
        {
            var _ = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)(dt.AddHours(-8) - _).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this uint ts)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(ts).AddHours(8);
        }

        /// <summary>
        /// 获取当前时间戳（时间戳单位秒）
        /// </summary>
        /// <returns></returns>
        public static uint GetCurrentTimeStamp()
        {
            var _ = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)(DateTime.Now.AddHours(-8) - _).TotalSeconds;
        }
    }

    /// <summary>
    /// IConfiguration扩展方法
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获取Config配置方法，只支持1级节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSectionValue<T>(this IConfiguration configuration) where T : AppSettings
        {
            var _ = configuration?.GetSection(typeof(T).Name);
            return _?.Get<T>();
        }

        /// <summary>
        /// 获取Config配置方法，最多支持2级节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="section">根节点</param>
        /// <param name="subsection">子节点</param>
        /// <returns></returns>
        public static T GetSectionValue<T>(this IConfiguration configuration, string section, string subsection = null) where T : class
        {
            if (string.IsNullOrEmpty(section))
                throw new ArgumentNullException(nameof(section));
            var _ = configuration?.GetSection(section);
            return string.IsNullOrEmpty(subsection) ? _?.Value as T : _?.Get<T>();
        }
    }

    /// <summary>
    /// IIApplicationBuilder扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// 请求拦截
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestIntercept(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestInterceptMiddleware>();
        }
    }

    public static class EncryptExtensions
    {
        /// <summary>
        /// 密码加密方案
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Md5Encrypt(this string source, string key = GlobalConfig.EncryptKey)
        {
            return GetMd5(GetMd5(source) + key);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetMd5(string source)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                //获取密文字节数组
                byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(source));
                //转换成字符串，32位
                string strResult = BitConverter.ToString(bytResult);
                //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
                strResult = strResult.Replace("-", "");
                return strResult.ToLower();
            }
        }
    }


    public static class RewriteOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static RewriteOptions AddRedirectToNonWww(this RewriteOptions options)
        {
            options.Rules.Add(new NonWwwRule());
            return options;
        }
    }

    internal class NonWwwRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var currentHost = req.Host;
            if (currentHost.Host.Equals("127.0.0.1") || currentHost.Host.Equals("localhost", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            if (Regex.IsMatch(currentHost.Host, @"(\w+\.)(.+\..+)", RegexOptions.Compiled))
            {
                string domain = Regex.Match(currentHost.Host, @"(\w+\.)(.+\..+)").Groups[2].Value;
                var newHost = new HostString(domain);
                var newUrl = new StringBuilder().Append("https://").Append(newHost).Append(req.PathBase).Append(req.Path).Append(req.QueryString);
                context.HttpContext.Response.Redirect(newUrl.ToString());
                context.Result = RuleResult.EndResponse;
            }
        }
    }

        /// <summary>
    /// Json序列化帮助类
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="missMemeberIgnore">忽略丢失的属性</param>
        /// <param name="referenceLoopIgnore">忽略循环引用</param>
        /// <param name="propertyCamelCase">驼峰式命名：针对C#与Js命名方式不一致</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json, bool missMemeberIgnore = true, bool referenceLoopIgnore = true, bool propertyCamelCase = true)
        {
            var setting = new JsonSerializerSettings();
            if (missMemeberIgnore)
                setting.MissingMemberHandling = MissingMemberHandling.Ignore;
            if (referenceLoopIgnore)
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            if (propertyCamelCase)
                setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nullValueIgnore">忽略空值类型</param>
        /// <param name="propertyCamelCase">驼峰式命名：针对C#与Js命名方式不一致</param>
        /// <returns></returns>
        public static string ObjectToJson(this object obj, bool nullValueIgnore = true, bool propertyCamelCase = true)
        {
            var setting = new JsonSerializerSettings();
            if (nullValueIgnore)
                setting.NullValueHandling = NullValueHandling.Ignore;
            if (propertyCamelCase)
                setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            try
            {
                return JsonConvert.SerializeObject(obj, setting);
            }
            catch
            {
                return null;
            }
        }
    }
}
