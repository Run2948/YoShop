using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;

namespace YoShop.Controllers
{
    public class CategoryController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public CategoryController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 商品分类管理

        /// <summary>
        /// 商品分类列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/index")]
        public async Task<IActionResult> Index()
        {
            var list = await _fsql.Select<Category>().ToListAsync();
            return View(list.Mapper<List<CategoryDto>>());
        }

        /// <summary>
        /// 商品分类添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/add")]
        public async Task<IActionResult> Add()
        {
            var list = await _fsql.Select<Category>().Where(l => l.ParentId == 0).ToListAsync();
            ViewData["first"] = list.Mapper<List<CategoryDto>>();
            return View(new CategoryDto());
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/add")]
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

            return YesRedirect("添加成功！", "/goods.category/index");
        }

        /// <summary>
        /// 商品分类编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/edit/categoryId/{id}")]
        public async Task<IActionResult> Edit(uint id)
        {
            var model = await _fsql.Select<Category>().Where(c => c.CategoryId == id).Include(c => c.UploadFile).ToOneAsync();
            if (model == null) return NoOrDeleted();
            var list = await _fsql.Select<Category>().Where(l => l.ParentId == 0).ToListAsync();
            ViewData["first"] = list.Mapper<List<CategoryDto>>();
            return View(model.Mapper<CategoryDto>());
        }

        /// <summary>
        /// 编辑商品分类
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/edit/categoryId/{id}")]
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
            return YesRedirect("编辑成功！", "/goods.category/index");
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/delete")]
        public async Task<IActionResult> Delete(uint categoryId)
        {
            try
            {
                await _fsql.Delete<Category>().Where(c => c.CategoryId == categoryId).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("删除成功！", "/goods.category/index");
        }
        #endregion
    }
}