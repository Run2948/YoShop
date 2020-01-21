using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class WxappController : ApiBaseController
    {
        private readonly IFreeSql _fsql;

        public WxappController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 基础信息
        /// </summary>
        /// <param name="wxappId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Base(uint wxappId)
        {
            var wxapp = await _fsql.Select<Wxapp>().Where(l => l.WxappId == wxappId).ToOneAsync();
            var navbar = await _fsql.Select<WxappNavbar>().DisableGlobalFilter().Where(l => l.WxappId == wxappId).ToOneAsync();
            return YesResult(new { wxapp, navbar });
        }

        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <param name="wxappId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Help(uint wxappId)
        {
            var list = await _fsql.Select<WxappHelp>().DisableGlobalFilter().Where(l=>l.WxappId == wxappId).OrderByDescending(l => l.Sort).ToListAsync();
            return YesResult(new { list });
        }

    }
}