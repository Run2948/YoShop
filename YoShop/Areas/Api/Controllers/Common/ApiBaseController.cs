using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using YoShop.Controllers;
using YoShop.WeChat;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class ApiBaseController : BaseController
    {
        [JsonProperty("wxappId")]
        public string WxappId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="context">The action executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            WxappId = context.HttpContext.Request.Headers["wxappId"].FirstOrDefault() ?? context.HttpContext.Request.Query["wxappId"].FirstOrDefault() ?? context.HttpContext.Request.Form["wxappId"].FirstOrDefault();
            Token = context.HttpContext.Request.Headers["token"].FirstOrDefault() ?? context.HttpContext.Request.Query["token"].FirstOrDefault() ?? context.HttpContext.Request.Form["token"].FirstOrDefault();
            if (string.IsNullOrEmpty(WxappId))
            {
                context.Result = No("缺少必要的参数：wxappId");
                return;
            }
            if (string.IsNullOrEmpty(Token))
            {
                context.Result = No("缺少必要的参数：token");
                return;
            }
            base.OnActionExecuting(context);
        }

        protected SnsResult GetWxUser(IMemoryCache cache)
        {
            cache.TryGetValue(Token, out SnsResult result);
            return result;
        }
    }
}