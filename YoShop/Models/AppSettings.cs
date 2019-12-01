using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    /// <summary>
    /// appsetings.json 文件中的配置项
    /// </summary>
    public class AppSettings { }

    /// <summary>
    /// 应用程序配置
    /// </summary>
    public class AppConfig : AppSettings
    {
        /// <summary>
        /// 调试模式
        /// </summary>
        public static bool IsDebug { get; set; }
    }
}
