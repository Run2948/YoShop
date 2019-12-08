using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Requests;

namespace YoShop.Controllers
{
    public class HomeController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public HomeController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 管理首页

        /// <summary>
        /// 管理首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/store.user/renew")]
        public IActionResult RenewPassword()
        {
            return View(new SellerPasswordRequest() { UserName = GetSellerSession().UserName });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("/store.user/renew"), AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RenewPassword(SellerPasswordRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.PasswordNew) || string.IsNullOrEmpty(request.PasswordConfirm))
                    return No("密码不能为空");
                if (request.PasswordConfirm != request.PasswordNew)
                    return No("密码不一致");
                var encryptPassword = request.Password.Md5Encrypt();
                var loginUser = await _fsql.Select<StoreUser>().Where(s => s.UserName == GetSellerSession().UserName && s.Password == encryptPassword).ToOneAsync();
                if (loginUser == null)
                    return No("原密码不正确");
                _fsql.Update<StoreUser>(loginUser.StoreUserId).Set(s => s.Password, request.PasswordNew.Md5Encrypt());
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("更新成功！");
        }

        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/passport/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionConfig.SellerInfo);
            Response.Cookies.Delete("seller_username");
            Response.Cookies.Delete("seller_password");
            HttpContext.Session.Clear();
            if (Request.Method.ToLower().Equals("get"))
                return Redirect("/passport/login");
            return Yes("注销成功！");
        }
        #endregion

    }
}
