using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Models;

namespace YoShop.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IFreeSql _fsql;

        public GoodsController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Index(int? page, int? size)
        {
            var select = _fsql.Select<Goods>();
            var total = await select.CountAsync();
            var list = await select.Include(g => g.Category).Page(page ?? 1, size ?? 15).ToListAsync();
            return View();
        }
    }
}