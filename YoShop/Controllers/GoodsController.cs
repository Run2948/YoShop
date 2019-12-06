using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Requests;
using YoShop.Models.Views;

namespace YoShop.Controllers
{
    public class GoodsController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public GoodsController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 商品列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods/index")]
        public async Task<IActionResult> Index(int? page, int? size)
        {
            var select = _fsql.Select<Goods>().Where(l => l.IsDelete == 0);
            var total = await select.CountAsync();
            var list = await select
                .Include(g => g.Category)
                .IncludeMany(g => g.GoodsImages, then => then.Include(i => i.UploadFile))
                .OrderByDescending(g => g.GoodsId)
                .Page(page ?? 1, size ?? 15).ToListAsync();
            return View(new GoodsListViewModel(list.Mapper<List<GoodsDto>>(), total));
        }

        /// <summary>
        /// 商品添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods/add")]
        public async Task<IActionResult> Add()
        {
            ViewData["category"] = await _fsql.Select<Category>().ToListAsync<CategorySelectDto>();
            ViewData["delivery"] = await _fsql.Select<Delivery>().ToListAsync<DeliverySelectDto>();
            return View(new SellerGoodsRequest());
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods/add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SellerGoodsRequest request)
        {
            request.WxappId = GetSellerSession().WxappId;
            request.CreateTime = DateTime.Now;
            request.UpdateTime = DateTime.Now;

            try
            {
                // 保存商品
                var goods = request.Mapper<Goods>();
                var goodsId = await _fsql.Insert<Goods>().AppendData(goods).ExecuteIdentityAsync();
                // 保存图片
                var goodsImages = request.BuildGoodsImages((uint)goodsId);
                if (goodsImages != null && goodsImages.Any())
                    await _fsql.Insert<GoodsImage>().AppendData(goodsImages).ExecuteAffrowsAsync();
                // 保存规格
                if (request.SpecType == SpecType.单规格)
                {
                    var goodsSpec = request.BuildGoodsSpec((uint)goodsId);
                    await _fsql.Insert<GoodsSpec>().AppendData(goodsSpec).ExecuteAffrowsAsync();
                }
                else
                {
                    var goodsSpecs = request.BuildGoodsSpecs((uint)goodsId);
                    await _fsql.Insert<GoodsSpec>().AppendData(goodsSpecs).ExecuteAffrowsAsync();
                    var goodsSpecRels = request.BuildGoodsSpecRels((uint)goodsId);
                    await _fsql.Insert<GoodsSpecRel>().AppendData(goodsSpecRels).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("添加成功！", "/goods/index");
        }

        /// <summary>
        /// 商品编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("/goods/edit/goodsId/{id}")]
        public async Task<IActionResult> Edit(uint id)
        {
            var goods = await _fsql.Select<Goods>().Where(c => c.GoodsId == id).ToOneAsync();
            if (goods == null) return NoOrDeleted();
            var model = goods.Mapper<SellerGoodsRequest>();
            var images = await _fsql.Select<GoodsImage>().Include(l => l.UploadFile).Where(l => l.GoodsId == id).ToListAsync();
            if (images.Any())
                model.ImageIds = images.Select(l => l.ImageId).ToArray();
            model.GoodsImages = images;
            model.GoodsSpec = new GoodsSpecDto();
            if (model.SpecType == SpecType.单规格)
            {
                var goodsSpec = await _fsql.Select<GoodsSpec>().Where(s => s.GoodsId == id).ToOneAsync();
                model.GoodsSpec = goodsSpec.Mapper<GoodsSpecDto>();
            }
            ViewData["category"] = await _fsql.Select<Category>().ToListAsync<CategorySelectDto>();
            ViewData["delivery"] = await _fsql.Select<Delivery>().ToListAsync<DeliverySelectDto>();
            return View(model);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods/edit/goodsId/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SellerGoodsRequest request, uint id)
        {
            var goods = await _fsql.Select<Goods>().Where(c => c.GoodsId == id).ToOneAsync();
            if (goods == null) return NoOrDeleted();
            try
            {
                request.GoodsId = goods.GoodsId;
                request.WxappId = goods.WxappId;
                request.CreateTime = goods.CreateTime.ConvertToDateTime();
                request.UpdateTime = DateTime.Now;

                // 更新商品
                var model = request.Mapper<Goods>();
                await _fsql.Update<Goods>().SetSource(model).ExecuteAffrowsAsync();
                // 更新图片
                await _fsql.Delete<GoodsImage>().Where(l => l.GoodsId == goods.GoodsId).ExecuteAffrowsAsync();
                var goodsImages = request.BuildGoodsImages(goods.GoodsId);
                if (goodsImages != null && goodsImages.Any())
                    await _fsql.Insert<GoodsImage>().AppendData(goodsImages).ExecuteAffrowsAsync();
                // 更新规格
                await _fsql.Delete<GoodsSpec>().Where(l => l.GoodsId == goods.GoodsId).ExecuteAffrowsAsync();
                await _fsql.Delete<GoodsSpecRel>().Where(l => l.GoodsId == goods.GoodsId).ExecuteAffrowsAsync();
                if (request.SpecType == SpecType.单规格)
                {
                    var goodsSpec = request.BuildGoodsSpec(goods.GoodsId);
                    await _fsql.Insert<GoodsSpec>().AppendData(goodsSpec).ExecuteAffrowsAsync();
                }
                else
                {
                    var goodsSpecs = request.BuildGoodsSpecs(goods.GoodsId);
                    await _fsql.Insert<GoodsSpec>().AppendData(goodsSpecs).ExecuteAffrowsAsync();
                    var goodsSpecRels = request.BuildGoodsSpecRels(goods.GoodsId);
                    await _fsql.Insert<GoodsSpecRel>().AppendData(goodsSpecRels).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("编辑成功！", "/goods/index");
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods/delete")]
        public async Task<IActionResult> Delete(uint goodsId)
        {
            try
            {
                await _fsql.Update<Goods>().Set(g => g.IsDelete == 1).Where(g => g.GoodsId == goodsId).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("删除成功！", "/goods/index");
        }
    }
}