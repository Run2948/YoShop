using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models.Requests
{
    /// <summary>
    /// 商家登录请求
    /// </summary>
    public class SellerLoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public string Remember { get; set; } = "on";
    }
}
