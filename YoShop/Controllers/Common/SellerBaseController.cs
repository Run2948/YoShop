using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Core.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YoShop.Extensions.Common;
using YoShop.Models;

namespace YoShop.Controllers
{
    /// <summary>
    /// 卖家公共控制器
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SellerBaseController : BaseController
    {
        #region 卖家用户Session相关操作

        /// <summary>
        /// 卖家是否登录
        /// </summary>
        /// <returns></returns>
        protected bool IsSellerLogin()
        {
            return GetSellerSession() != null;
        }

        /// <summary>
        /// 获取卖家登录信息
        /// </summary>
        /// <returns></returns>
        protected StoreUserDto GetSellerSession()
        {
            return HttpContext.Session.Get<StoreUserDto>(SessionConfig.SellerInfo);
        }

        /// <summary>
        /// 设置卖家登录信息
        /// </summary>
        /// <param name="dto"></param>
        protected void SetSellerSession(StoreUserDto dto)
        {
            HttpContext.Session.Set(SessionConfig.SellerInfo, dto);
        }

        /// <summary>
        /// 注销卖家账户
        /// </summary>
        protected void SetSellerLogOut()
        {
            HttpContext.Session.Remove(SessionConfig.SellerInfo);
            Response.Cookies.Delete("seller_username");
            Response.Cookies.Delete("seller_password");
            HttpContext.Session.Clear();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (AppConfig.IsDebug)
            {
                if (!IsSellerLogin())
                {
                    var sellerDto = new StoreUserDto() { StoreUserId = 10001, WxappId = 10001, UserName = "seller" };
                    context.HttpContext.Session.Set(SessionConfig.SellerInfo, sellerDto);
                }
            }
            base.OnActionExecuting(context);
        }

    }
}