using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models.Requests
{
    public class SellerPasswordRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string PasswordNew { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string PasswordConfirm { get; set; }
    }
}
