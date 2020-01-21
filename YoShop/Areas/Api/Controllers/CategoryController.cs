using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class CategoryController : ApiBaseController
    {
        private readonly IFreeSql _fsql;

        public CategoryController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Lists(uint wxappId)
        {
            var list = await _fsql.Select<Category>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.ParentId == 0).Include(l => l.Image).IncludeMany(l => l.Child, then => then.Include(x => x.Image)).ToListAsync();
            return YesResult(new { list });
        }
    }
}