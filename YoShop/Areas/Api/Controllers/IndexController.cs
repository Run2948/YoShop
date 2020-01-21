using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class IndexController : ApiBaseController
    {
        private readonly IFreeSql _fsql;

        public IndexController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Page(uint wxappId)
        {
            // 页面元素
            var page = await _fsql.Select<WxappPage>().DisableGlobalFilter().Where(l => l.WxappId == wxappId).ToOneAsync();
            // 新品推荐
            var newest = await _fsql.Select<Goods>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.IsDelete == 0 && l.GoodsStatus == 10)
                .OrderByDescending(l => l.GoodsId).OrderBy(l => l.GoodsSort)
                .Include(l => l.Category)
                .IncludeMany(l => l.GoodsImages, then => then.Include(x => x.UploadFile))
                .IncludeMany(l => l.GoodsSpecs)
                .ToListAsync();
            // 猜您喜欢
            var best = await _fsql.Select<Goods>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.IsDelete == 0 && l.GoodsStatus == 10).Limit(10)
                .OrderByDescending(l => l.SalesInitial).OrderBy(l => l.GoodsSort)
                .Include(l => l.Category)
                .IncludeMany(l => l.GoodsImages, then => then.Include(x => x.UploadFile))
                .IncludeMany(l => l.GoodsSpecs)
                .ToListAsync();
            return YesResult(new { items = JObject.Parse(page.PageData)["items"], newest, best });
        }
    }
}