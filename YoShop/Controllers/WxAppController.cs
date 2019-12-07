using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions;
using YoShop.Models;

namespace YoShop.Controllers
{
    public class WxappController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public WxappController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 小程序设置

        [HttpGet, Route("/wxapp/setting")]
        public async Task<IActionResult> Setting()
        {
            var wxapp = await _fsql.Select<Wxapp>().ToOneAsync() ?? new Wxapp();
            return View(wxapp);
        }

        [HttpPost, Route("/wxapp/setting")]
        public async Task<IActionResult> Setting(Wxapp model)
        {
            try
            {
                var wxapp = await _fsql.Select<Wxapp>().ToOneAsync() ?? new Wxapp();
                wxapp.AppId = model.AppId;
                wxapp.AppSecret = model.AppSecret;
                wxapp.MchId = model.MchId;
                wxapp.ApiKey = model.ApiKey;
                wxapp.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                if (wxapp.WxappId == 0)
                {
                    wxapp.CreateTime = DateTime.Now.ConvertToTimeStamp();
                    await _fsql.Insert<Wxapp>().AppendData(wxapp).ExecuteAffrowsAsync();
                }
                else
                {
                    wxapp.WxappId = GetSellerSession().WxappId;
                    await _fsql.Update<Wxapp>().SetSource(wxapp).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("保存成功！", "/wxapp/setting");
        }

        #endregion

        #region 首页设计

        [HttpGet, Route("/wxapp.page/home")]
        public async Task<IActionResult> Home()
        {
            var page = await _fsql.Select<WxappPage>().ToOneAsync() ?? new WxappPage();
            return View(page);
        }

        [HttpPost, Route("/wxapp.page/home")]
        public async Task<IActionResult> Home(string data)
        {
            try
            {
                var page = await _fsql.Select<WxappPage>().ToOneAsync() ?? new WxappPage();
                page.PageData = data;
                page.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                if (page.PageId == 0)
                {
                    page.PageType = 10;
                    page.CreateTime = DateTime.Now.ConvertToTimeStamp();
                    await _fsql.Insert<WxappPage>().AppendData(page).ExecuteAffrowsAsync();
                }
                else
                {
                    page.WxappId = GetSellerSession().WxappId;
                    await _fsql.Update<WxappPage>().SetSource(page).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("保存成功！", "/wxapp.page/home");
        }

        #endregion

        #region 页面链接

        [HttpGet, Route("/wxapp.page/links")]
        public async Task<IActionResult> Links()
        {
            return await Task.Run(View);
        }

        #endregion

        #region 导航设置

        [HttpGet, Route("/wxapp/navbar")]
        public async Task<IActionResult> Navbar()
        {
            var navbar = await _fsql.Select<WxappNavbar>().ToOneAsync() ?? new WxappNavbar();
            return View(navbar);
        }

        [HttpPost, Route("/wxapp/navbar")]
        public async Task<IActionResult> Navbar(WxappNavbar model)
        {
            try
            {
                var navbar = await _fsql.Select<WxappNavbar>().ToOneAsync() ?? new WxappNavbar();
                navbar.WxappTitle = model.WxappTitle;
                navbar.TopTextColor = model.TopTextColor;
                navbar.TopBackgroundColor = model.TopBackgroundColor;
                navbar.UpdateTime = DateTime.Now.ConvertToTimeStamp();
                if (navbar.NavbarId == 0)
                {
                    navbar.CreateTime = DateTime.Now.ConvertToTimeStamp();
                    navbar.WxappId = GetSellerSession().WxappId;
                    await _fsql.Insert<WxappNavbar>().AppendData(navbar).ExecuteAffrowsAsync();
                }
                else
                {
                    await _fsql.Update<WxappNavbar>().SetSource(navbar).ExecuteAffrowsAsync();
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("保存成功！", "/wxapp/navbar");
        }

        #endregion
    }
}