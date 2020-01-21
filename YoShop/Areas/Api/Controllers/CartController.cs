using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YoShop.Areas.Api.Models;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    public class CartController : UserBaseController
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IFreeSql _fsql;

        public CartController(IMemoryCache memoryCache, IFreeSql fsql)
            : base(memoryCache)
        {
            _memoryCache = memoryCache;
            _fsql = fsql;
        }

        public async Task<IActionResult> Lists(uint wxappId)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.OpenId == SessionKey.OpenId).ToOneAsync();
            var address = await _fsql.Select<UserAddress>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.UserId == user.UserId)
                .Include(l => l.Region)
                .ToOneAsync();
            var goodsList = new List<object>();
            return YesResult(new
            {
                address,
                error_msg = "",
                exist_address = address == null,
                express_price = 0,
                goods_list = goodsList,
                has_error = false,
                intra_region = true,
                order_pay_price = "0.00",
                order_total_num = 0,
                order_total_price = 0,
            });
        }

        public async Task<IActionResult> Add(uint wxappId, uint goodsId, int goodsNum, string goodsSkuId)
        {
            var dto = new CartDto();
            // 购物车商品索引
            var index = $"{goodsId}_{goodsSkuId}";
            // 商品信息
            var goods = await _fsql.Select<Goods>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.GoodsId == goodsId).ToOneAsync();
            // 商品sku信息
            //$goods['goods_sku'] = $goods->getGoodsSku($goods_sku_id);
            // 判断商品是否下架
            if (goods.GoodsStatus != 10)
            {
                dto.ErrorMsg = "很抱歉，该商品已下架";
                return No("");
            }

            return View();
        }
    }
}