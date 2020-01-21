using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    [Area("api")]
    public class GoodsController : ApiBaseController
    {
        private readonly IFreeSql _fsql;

        public GoodsController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Detail(uint wxappId, uint goodsId)
        {
            var detail = await _fsql.Select<Goods>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.GoodsId == goodsId)
                .OrderByDescending(l => l.GoodsId).OrderBy(l => l.GoodsSort)
                .Include(l => l.Category)
                .Include(l => l.Delivery)
                .IncludeMany(l => l.GoodsImages, then => then.Include(x => x.UploadFile))
                .IncludeMany(l => l.GoodsSpecs)
                .ToOneAsync();
            if (detail == null) return YesResult("未找到该商品");
            return YesResult(new { detail, specData = JObject.Parse(detail.SpecMany ?? "{\"spec_attr\":[],\"spec_list\":[]}") });
        }

    }
}