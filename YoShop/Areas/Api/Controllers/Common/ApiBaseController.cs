using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YoShop.Controllers;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class ApiBaseController : BaseController
    {
        public string WxappId { get; set; }

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="context">The action executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            WxappId = context.HttpContext.Request.Headers["wxappId"].FirstOrDefault() ?? context.HttpContext.Request.Query["wxappId"].FirstOrDefault() ?? context.HttpContext.Request.Form["wxappId"].FirstOrDefault();
            if (string.IsNullOrEmpty(WxappId))
            {
                context.Result = No("缺少必要的参数：wxappId");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}