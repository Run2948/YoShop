using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Masuit.Tools;
using Masuit.Tools.AspNetCore.ResumeFileResults.Extensions;
using Masuit.Tools.Core.Net;
using Masuit.Tools.Strings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Requests;

namespace YoShop.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IFreeSql _fsql;

        public LoginController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/passport/login")]
        public async Task<IActionResult> Index()
        {
            string from = Request.Query["from"];
            if (!string.IsNullOrEmpty(from))
            {
                from = HttpUtility.UrlDecode(from);
                Response.Cookies.Append("refer", from);
            }
            if (HttpContext.Session.Get<StoreUserDto>(SessionConfig.SellerInfo) != null)
            {
                if (string.IsNullOrEmpty(from))
                    from = "/";
                return Redirect(from);
            }
            if (Request.Cookies.Count > 2)
            {
                string name = Request.Cookies["seller_username"];
                string pwd = Request.Cookies["seller_password"];
                var loginSeller = await _fsql.Select<StoreUser>().Where(s => s.UserName == HttpUtility.HtmlDecode(name) && s.Password == pwd).ToOneAsync();
                if (loginSeller != null)
                {
                    Response.Cookies.Append("seller_username", name, new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Append("seller_password", Request.Cookies["seller_password"], new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    HttpContext.Session.Set(SessionConfig.SellerInfo, loginSeller.Mapper<StoreUserDto>());
                    //初始化系统设置参数
                    GlobalConfig.TalentId = loginSeller.WxappId;
                    var settings = await _fsql.Select<Setting>().ToListAsync();
                    GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
                    var wxapp = await _fsql.Select<Wxapp>().Where(l => l.WxappId == GlobalConfig.TalentId).ToOneAsync();
                    GlobalConfig.WxappConfig = wxapp.Mapper<WxappConfig>();
                    if (string.IsNullOrEmpty(from))
                        from = "/";
                    return Redirect(from);
                }
            }
            return View(new SellerLoginRequest());
        }

        /// <summary>
        /// 登陆检查
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("/passport/login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SellerLoginRequest request)
        {
            //string validSession = HttpContext.Session.Get<string>("valid") ?? string.Empty; //将验证码从Session中取出来，用于登录验证比较
            //if (string.IsNullOrEmpty(validSession) || !valid.Trim().Equals(validSession, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    return No("验证码错误");
            //}
            //HttpContext.Session.Remove("valid"); //验证成功就销毁验证码Session，非常重要
            if (string.IsNullOrEmpty(request.UserName.Trim()) || string.IsNullOrEmpty(request.Password.Trim()))
                return No("用户名或密码不能为空");
            var encryptPassword = request.Password.Md5Encrypt();
            var loginSeller = await _fsql.Select<StoreUser>().Where(l => l.UserName == request.UserName && l.Password == encryptPassword).ToOneAsync();
            if (loginSeller != null)
            {
                HttpContext.Session.Set(SessionConfig.SellerInfo, loginSeller.Mapper<StoreUserDto>());
                if (request.Remember.Trim().Contains(new[] { "on", "true" })) //是否记住登录
                {
                    Response.Cookies.Append("seller_username", HttpUtility.UrlEncode(request.UserName.Trim()), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Append("seller_password", encryptPassword, new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                }
                string refer = Request.Cookies["refer"];
                //初始化系统设置参数
                GlobalConfig.TalentId = loginSeller.WxappId;
                var settings = await _fsql.Select<Setting>().ToListAsync();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
                var wxapp = await _fsql.Select<Wxapp>().Where(l => l.WxappId == GlobalConfig.TalentId).ToOneAsync();
                GlobalConfig.WxappConfig = wxapp.Mapper<WxappConfig>();
                return YesRedirect("登录成功！", string.IsNullOrEmpty(refer) ? "/" : refer);
            }
            return No("用户名或密码错误");
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = Masuit.Tools.Strings.ValidateCode.CreateValidateCode(6);
            HttpContext.Session.Set("valid", code); //将验证码生成到Session中
            var buffer = HttpContext.CreateValidateGraphic(code);
            return this.ResumeFile(buffer, "image/jpeg");
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckValidateCode(string code)
        {
            string validSession = HttpContext.Session.Get<string>("valid");
            if (string.IsNullOrEmpty(validSession) || !code.Trim().Equals(validSession, StringComparison.InvariantCultureIgnoreCase))
            {
                return No("验证码错误");
            }
            return Yes("验证码正确");
        }
    }
}