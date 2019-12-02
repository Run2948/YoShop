using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YoShop.Extensions.Common;
using YoShop.Models;
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
        public async Task<IActionResult> Add()
        {
            await _fsql.Select<Region>().ToListAsync();
            return View();
        }

        /// <summary>
        /// 保存配送设置
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DeliveryDto viewModel)
        {
            return Yes("保存成功");
        }

        /// <summary>
        /// 编辑配送设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/edit/deliveryId/{id}")]
        public async Task<IActionResult> Edit(uint id)
        {
            return View();
        }

        /// <summary>
        /// 更新配送设置
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/edit/deliveryId/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string str, uint id)
        {
            return Yes("更新成功");
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
                await _fsql.Delete<Delivery>().Where(d => d.DeliveryId == deliveryId).ExecuteAffrowsAsync();
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