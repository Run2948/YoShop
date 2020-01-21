using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using YoShop.Models;
using YoShop.WeChat;

namespace YoShop.Areas.Api.Controllers
{
    public class UserBaseController : ApiBaseController
    {
        private readonly IMemoryCache _memoryCache;

        public UserBaseController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public SnsResult SessionKey { get; set; }

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="context">The action executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["token"].FirstOrDefault() ?? context.HttpContext.Request.Query["token"].FirstOrDefault() ?? context.HttpContext.Request.Form["token"].FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = Fail("缺少必要的参数：token");
                return;
            }

            SessionKey = _memoryCache.Get<SnsResult>(token);

            if (AppConfig.IsDebug)
            {
                SessionKey = new SnsResult() { OpenId = "o2l7O0RYvxQ-5zvC4ucq8uqy_h_M" };
            }

            if (SessionKey == null)
            {
                context.Result = Fail("请登录");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}