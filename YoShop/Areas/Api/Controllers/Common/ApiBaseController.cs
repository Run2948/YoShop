using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using YoShop.Controllers;
using YoShop.Extensions;
using YoShop.Extensions.Common;
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
            WxappId = context.ActionArguments["wxappId"]?.ToString();
            if (string.IsNullOrEmpty(WxappId))
            {
                context.Result = No("缺少必要的参数：wxappId");
                return;
            }
            Token = context.ActionArguments["token"]?.ToString();
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