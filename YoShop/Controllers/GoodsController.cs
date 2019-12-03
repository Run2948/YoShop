using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
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
            var select = _fsql.Select<Goods>();
            var total = await select.CountAsync();
            var list = await select
                .Include(g => g.Category)
                .IncludeMany(g => g.GoodsImages, then => then.Include(i => i.UploadFile))
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
            return View(new GoodsDto());
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods/add"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryDto viewModel)
        {
            viewModel.WxappId = GetSellerSession().WxappId;
            viewModel.CreateTime = DateTime.Now;
            viewModel.UpdateTime = DateTime.Now;
            try
            {
                var model = viewModel.Mapper<Category>();
                await _fsql.Insert<Category>().AppendData(model).ExecuteAffrowsAsync();
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
            var model = await _fsql.Select<Category>().Where(c => c.CategoryId == id).Include(c => c.UploadFile).ToOneAsync();
            if (model == null) return NoOrDeleted();
            var list = await _fsql.Select<Category>().Where(l => l.ParentId == 0).ToListAsync();
            ViewData["first"] = list.Mapper<List<CategoryDto>>();
            return View(model.Mapper<CategoryDto>());
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods/edit/goodsId/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto viewModel, uint id)
        {
            var model = await _fsql.Select<Category>().Where(c => c.CategoryId == id).ToOneAsync();
            if (model == null) return NoOrDeleted();
            try
            {
                model.Name = viewModel.Name;
                model.ParentId = viewModel.ParentId;
                model.Sort = viewModel.Sort;
                model.ImageId = viewModel.ImageId;
                model.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                await _fsql.Update<Category>().SetSource(model).ExecuteAffrowsAsync();
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
            return YesRedirect("删除成功！", "/goods.category/index");
        }
    }
}