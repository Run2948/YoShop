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
    public class HelpController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public HelpController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 帮助中心

        [HttpGet, Route("/wxapp.help/index")]
        public async Task<IActionResult> Index()
        {
            var list = await _fsql.Select<WxappHelp>().OrderBy(u => u.HelpId).ToListAsync();
            return View(list);
        }


        [HttpGet, Route("/wxapp.help/add")]
        public async Task<IActionResult> Add()
        {
            return await Task.Run(() => View(new WxappHelp()));
        }

        [HttpPost, Route("/wxapp.help/add")]
        public async Task<IActionResult> Add(WxappHelp help)
        {
            try
            {
                help.WxappId = GetSellerSession().WxappId;
                help.CreateTime = DateTime.Now.ConvertToTimeStamp();
                help.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                await _fsql.Insert<WxappHelp>().AppendData(help).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return YesRedirect("添加成功！", "/wxapp.help/index");
        }


        [HttpGet, Route("/wxapp.help/edit/helpId/{id}")]
        public async Task<IActionResult> Edit(uint id)
        {
            var model = await _fsql.Select<WxappHelp>().Where(l => l.HelpId == id).ToOneAsync();
            if (model == null) return NoOrDeleted();
            return View(model);
        }

        [HttpPost, Route("/wxapp.help/edit/helpId/{id}")]
        public async Task<IActionResult> Edit(WxappHelp help, uint id)
        {
            var model = await _fsql.Select<WxappHelp>().Where(l => l.HelpId == id).ToOneAsync();
            if (model == null) return NoOrDeleted();
            try
            {
                model.Title = help.Title;
                model.Content = help.Content;
                model.Sort = help.Sort;
                model.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                await _fsql.Update<WxappHelp>().SetSource(model).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("编辑成功！", "/wxapp.help/index");
        }


        [HttpPost, Route("/wxapp.help/delete")]
        public async Task<IActionResult> Delete(uint helpId)
        {
            try
            {
                await _fsql.Delete<WxappHelp>().Where(l => l.HelpId == helpId).ExecuteAffrowsAsync();
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("删除成功！", "/wxapp.help/index");
        }

        #endregion
    }
}