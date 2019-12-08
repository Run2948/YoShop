using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Controllers;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    public class WxappController : BaseController
    {
        private readonly IFreeSql _fsql;

        public WxappController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 基础信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Base()
        {
            var wxapp = await _fsql.Select<Wxapp>().ToOneAsync();
            return YesResult(wxapp);
        }

        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Help()
        {
            var list = await _fsql.Select<WxappHelp>().OrderByDescending(l => l.Sort).ToListAsync();
            return YesResult(list);
        }

    }
}