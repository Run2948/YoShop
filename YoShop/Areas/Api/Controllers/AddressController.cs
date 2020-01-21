using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YoShop.Areas.Api.Models.Requests;
using YoShop.Models;

namespace YoShop.Areas.Api.Controllers
{
    public class AddressController : UserBaseController
    {
        private readonly IFreeSql _fsql;
        public AddressController(IFreeSql fsql, IMemoryCache memoryCache)
            : base(memoryCache)
        {
            _fsql = fsql;
        }

        public async Task<IActionResult> Lists(uint wxappId)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.OpenId == SessionKey.OpenId).ToOneAsync();
            var list = await _fsql.Select<UserAddress>().DisableGlobalFilter().Include(l => l.Region).Where(l => l.WxappId == wxappId && l.UserId == user.UserId).ToListAsync();
            return YesResult(new { default_id = user.AddressId, list });
        }

        public async Task<IActionResult> Detail(uint addressId, uint wxappId)
        {
            var detail = await _fsql.Select<UserAddress>().DisableGlobalFilter().Include(l => l.Region).Where(l => l.WxappId == wxappId && l.AddressId == addressId).ToOneAsync();
            var region = detail.Region.MergerName.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().Skip(1).ToArray();
            return YesResult(new { detail, region });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddressRequest request)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter().Where(l => l.WxappId == request.WxappId && l.OpenId == SessionKey.OpenId).ToOneAsync();
            var regionIds = await GetRegionIdByNames(request.Region);
            var address = new UserAddress
            {
                UserId = user.UserId,
                Name = request.Name,
                Phone = request.Phone,
                Detail = request.Detail,
                WxappId = request.WxappId,
                ProvinceId = regionIds[0],
                CityId = regionIds[1],
                AddressId = regionIds[2]
            };
            await _fsql.Insert<UserAddress>().AppendData(address).ExecuteAffrowsAsync();
            return Yes("添加成功");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddressRequest request)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter().Where(l => l.WxappId == request.WxappId && l.OpenId == SessionKey.OpenId).ToOneAsync();
            var regionIds = await GetRegionIdByNames(request.Region);
            var address = new UserAddress
            {
                AddressId = request.AddressId,
                UserId = user.UserId,
                Name = request.Name,
                Phone = request.Phone,
                Detail = request.Detail,
                WxappId = request.WxappId,
                ProvinceId = regionIds[0],
                CityId = regionIds[1],
                RegionId = regionIds[2]
            };
            await _fsql.Insert<UserAddress>().AppendData(address).ExecuteAffrowsAsync();
            return Yes("编辑成功");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(uint addressId)
        {
            await _fsql.Delete<UserAddress>().DisableGlobalFilter().Where(l => l.WxappId == WxappId.ToUInt32() && l.AddressId == addressId).ExecuteAffrowsAsync();
            return Yes("删除成功");
        }

        [HttpPost]
        public async Task<IActionResult> SetDefault(uint addressId, uint wxappId)
        {
            var user = await _fsql.Select<User>().DisableGlobalFilter().Where(l => l.WxappId == wxappId && l.OpenId == SessionKey.OpenId).ToOneAsync();
            await _fsql.Update<User>().DisableGlobalFilter().Set(l => l.AddressId == addressId).Where(l => l.WxappId == wxappId && l.UserId == user.UserId).ExecuteAffrowsAsync();
            return Yes("设置成功");
        }


        private async Task<List<uint>> GetRegionIdByNames(string regionName)
        {
            if (!regionName.Contains(','))
                return await _fsql.Select<Region>().Where(l => l.Name == regionName).ToListAsync(l => l.Id);
            var regions = regionName.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return await _fsql.Select<Region>().Where(l => regions.Contains(l.Name)).ToListAsync(l => l.Id);
        }
    }
}