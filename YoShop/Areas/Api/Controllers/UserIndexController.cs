using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    [Route("{area}/User.Index/[action]")]
    public class UserIndexController : UserBaseController
    {
        private readonly IFreeSql _fsql;
        public UserIndexController(IFreeSql fsql, IMemoryCache memoryCache)
        : base(memoryCache)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Detail(uint wxappId)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter()
                .Where(l => l.WxappId == wxappId && l.OpenId == SessionKey.OpenId)
                .Include(l => l.DefaultAddress.Region)
                .IncludeMany(l => l.UserAddress, then => then.Include(l => l.Region))
                .ToOneAsync();
            var paymentCount = await GetOrderCount(wxappId, "payment");
            var receivedCount = await GetOrderCount(wxappId, "received");
            return YesResult(new { userInfo = user, orderCount = new { payment = paymentCount, received = receivedCount } });
        }

        private async Task<long> GetOrderCount(uint wxappId, string type = "all")
        {
            Expression<Func<Order, bool>> where = l => l.WxappId == wxappId;
            switch (type)
            {
                case "payment":
                    where = where.And(l => l.PayStatus == 10);
                    break;
                case "received":
                    where = where.And(l => l.PayStatus == 20 && l.DeliveryStatus == 20 && l.ReceiptStatus == 10);
                    break;
                case "all":
                    break;
            }
            return await _fsql.Select<Order>().DisableGlobalFilter().Where(where).CountAsync();
        }
    }
}