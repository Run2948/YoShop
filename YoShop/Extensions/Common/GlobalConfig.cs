using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace YoShop.Extensions.Common
{
    public static class GlobalConfig
    {
        /// <summary>
        ///  管理员密码密钥
        /// </summary>
        public const string EncryptKey = "yoshop_salt_SmTRx";

        /// <summary>
        /// 系统设定
        /// </summary>
        public static Dictionary<string, JObject> SystemSettings { get; set; }

        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Mapper<T>(this object source) where T : class => AutoMapper.Mapper.Map<T>(source);

        /// <summary>
        /// Enum 映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this object source) where T : Enum => (T)Enum.ToObject(typeof(T),source);

        /// <summary>
        /// 网站启动时间
        /// </summary>
        public static DateTime StartupTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 网站租户Id
        /// </summary>
        public static uint TalentId { get; set; } = 10001;

        /// <summary>
        /// 访问量
        /// </summary>
        public static double InterviewCount
        {
            get
            {
                try
                {
                    return RedisHelper.Get<double>("Interview:ViewCount");
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    value = RedisHelper.IncrBy("Interview:ViewCount");
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// 是否是机器人访问
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static bool IsRobot(this HttpRequest req)
        {
            return req.Headers[HeaderNames.UserAgent].ToString().Contains(new[]
            {
                "DNSPod",
                "Baidu",
                "spider",
                "Python",
                "bot"
            });
        }
    }
}
