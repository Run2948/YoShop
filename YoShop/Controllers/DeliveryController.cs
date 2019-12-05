using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Requests;
using YoShop.Models.Views;

namespace YoShop.Controllers
{
    public class DeliveryController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public DeliveryController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 配送设置

        /// <summary>
        /// 配送设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/index")]
        public async Task<IActionResult> Index(int? page, int? size)
        {
            var select = _fsql.Select<Delivery>();
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 15).OrderBy(u => u.DeliveryId).ToListAsync();
            return View(new DeliveryListViewModel(list.Mapper<List<DeliveryDto>>(), total));
        }

        /// <summary>
        /// 新增配送设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/add")]
        public IActionResult Add()
        {
            return View(new DeliveryDto());
        }

        /// <summary>
        /// 保存配送设置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SellerDeliveryRequest request)
        {
            request.WxappId = GetSellerSession().WxappId;
            request.CreateTime = DateTime.Now;
            request.UpdateTime = DateTime.Now;
            try
            {
                var deliveryId = await _fsql.Insert<Delivery>().AppendData(request.Mapper<Delivery>()).ExecuteIdentityAsync();
                var deliveryRules = request.BuildDeliveryRules((uint)deliveryId);
                await _fsql.Insert<DeliveryRule>().AppendData(deliveryRules).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("保存成功！", "/setting.delivery/index");
        }

        /// <summary>
        /// 编辑配送设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/edit/deliveryId/{id}")]
        public async Task<IActionResult> Edit(uint id)
        {
            var delivery = await _fsql.Select<Delivery>().Where(l => l.DeliveryId == id).ToOneAsync();
            var ruleWithRegions = await _fsql.Select<DeliveryRule>().Where(l => l.DeliveryId == id).ToListAsync(l => new DeliveryRuleWithRegionDto()
            {
                Region = l.Region,
                First = l.First,
                FirstFee = l.FirstFee,
                Additional = l.Additional,
                AdditionalFee = l.AdditionalFee
            });
            foreach (var ruleWithRegion in ruleWithRegions)
            {
                if (!string.IsNullOrEmpty(ruleWithRegion.Region))
                {
                    var regionIds = ruleWithRegion.Region.Split(',').Select(l => Convert.ToUInt32(l)).ToList();
                    if (regionIds.Any())
                    {
                        var parentId = await _fsql.Select<Region>().Where(l => l.Id == regionIds[0]).ToOneAsync(l => l.Pid);
                        var province = await _fsql.Select<Region>().Where(l => l.Id == parentId).ToOneAsync(l => l.Name);
                        var cities = await _fsql.Select<Region>().Where(l => regionIds.Contains(l.Id)).ToListAsync(l => l.Name);
                        ruleWithRegion.Content = $"{province} (<span class=\"am-link-muted\">{string.Join("、", cities)}</span>)";
                    }
                }
            }
            ViewData["rules"] = ruleWithRegions;
            return View(delivery.Mapper<DeliveryDto>());
        }

        /// <summary>
        /// 更新配送设置
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/edit/deliveryId/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SellerDeliveryRequest request, uint id)
        {
            try
            {
                var delivery = await _fsql.Select<Delivery>().Where(l => l.DeliveryId == id).ToOneAsync();
                if (delivery == null) return No("该记录不存在或已被删除！");

                request.DeliveryId = delivery.DeliveryId;
                request.WxappId = delivery.WxappId;
                request.CreateTime = DateTime.Now;
                request.UpdateTime = DateTime.Now;

                delivery.Name = request.Name;
                delivery.Method = request.Method.ToByte();
                delivery.Sort = request.Sort;
                delivery.CreateTime = DateTime.Now.ConvertToTimeStamp();

                var count = await _fsql.Update<Delivery>().SetSource(delivery).ExecuteAffrowsAsync();
                if (count > 0)
                {
                    var deliveryRules = request.BuildDeliveryRules(delivery.DeliveryId);
                    count = await _fsql.Delete<DeliveryRule>().Where(l => l.DeliveryId == delivery.DeliveryId).ExecuteAffrowsAsync();
                    if (count > 0)
                        await _fsql.Insert<DeliveryRule>().AppendData(deliveryRules).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("更新成功！", "/setting.delivery/index");
        }

        /// <summary>
        /// 删除配送设置
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/delete")]
        public async Task<IActionResult> Delete(uint deliveryId)
        {
            try
            {
                long count = await _fsql.Delete<Delivery>().Where(d => d.DeliveryId == deliveryId).ExecuteAffrowsAsync();
                if (count > 0)
                    await _fsql.Delete<DeliveryRule>().Where(d => d.DeliveryId == deliveryId).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("删除成功！", "/setting.delivery/index");
        }

        #endregion

        #region 可配送区域

        [HttpGet, Route("/setting.delivery/delete/region")]
        public async Task<IActionResult> Region()
        {
            var all = await _fsql.Select<Region>().ToListAsync<RegionDto>();

            var provinces = all.Where(l => l.Pid == 0).ToList();

            foreach (var p in provinces)
            {
                var cities = all.Where(l => l.Pid == p.Id).ToList();
                if (cities.Any())
                {
                    p.City = new List<RegionDto>();
                    p.City = cities;
                }
            }

            foreach (var c in provinces.SelectMany(p => p.City))
            {
                var regions = all.Where(l => l.Pid == c.Id).ToList();
                if (regions.Any())
                {
                    c.Region = new List<RegionDto>();
                    c.Region = regions;
                }
            }

            return YesResult(provinces);
        }

        #endregion
    }
}