using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.WeChat;

namespace YoShop.Areas.Api.Controllers
{
    public class UserController : ApiBaseController
    {
        private readonly IFreeSql _fsql;
        private readonly WxHttpClient _wxHttpClient;
        private readonly IMemoryCache _memoryCache;

        public UserController(IFreeSql fsql, WxHttpClient wxHttpClient, IMemoryCache memoryCache)
        {
            _fsql = fsql;
            _wxHttpClient = wxHttpClient;
            _memoryCache = memoryCache;
        }

        /** 微信登录提交过来的数据
         {
	        "code": "043fZFgo12ns1p09egeo1NOOgo1fZFgP",
	        "user_info": "{\"nickName\":\"默者非靡\",\"gender\":1,\"language\":\"zh_CN\",\"city\":\"黄冈\",\"province\":\"湖北\",\"country\":\"中国\",\"avatarUrl\":\"https://wx.qlogo.cn/mmopen/vi_32/3Q7F0CTtgq05tZ9cXUc6AbESAhHLJwmeB7rHl86mZR21PicF5D9kYibj9bSkIkjPTRpXCMlXXUeicbxuicxzNiaqSRg/132\"}",
	        "encrypted_data": "+vbp9A2ghnuvwcMBQAatzFc85ExTiBdUqaYpJwKLGDMo0dgI/DJYEbM3J25a9Im0LqvezdYqXqzC/TKZ0FMPDuxTtWSWms+LuekyRqdINbcWr1tdXVxHGE+FhJpXAVGhdIrmJLns5z3yFgIvd3wUTC1DbTzAXwBICFxsuKZ8KrO/zDzgnpJbUyCV2flIpNT/ZuMKJ34QcA3A7S4+mL91IyH1xFts4u4+gNOzHP8iHLjk3Cv2h0aI0+OjgAEFK5PelyyEJHd6iUgXvxu4Q8OS94lMGo6/QBt4cedEABqi8BZpBEi2bmXMgH86oyUVE9/xu0t6mdGq5ltFh4AFpvT6SM2AI0dculb1V581icmW5xIdxFaakqO9mrxh2iVCn95k6VqYxwkMtswJ87jDOOlgNjQaMQ1/QrIpC/pAWn7WoOybH9makt5/3Zo0S8PAdWsH9DGjLjbvseLtR+ybSuLKIqDVQ4ZiqgVTGQwRnERjq+w=",
	        "iv": "JCoVIPDqRIhzTrwHuO4IJw==",
	        "signature": "19cb40bd165bc19d645695a71ce67f87bdae460d",
	        "wxapp_id": "10001",
	        "token": ""
        }
         */

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                // 微信登录 获取session_key
                var session = await WxLogin(request.WxappId, request.Code);
                // 自动注册用户
                var userId = await WxRegister(session.OpenId, request.UserInfo);
                // 生成token (session3rd)
                var token = WxToken(request.WxappId, session.OpenId);
                // 记录缓存, 7天
                _memoryCache.Set(token, session, TimeSpan.FromDays(7));
                return YesResult(new { userId, token });
            }
            catch (Exception e)
            {
                return Fail(e.Message);
            }
        }

        /// <summary>
        /// 自动注册用户
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private async Task<long> WxRegister(string openId, string userInfo)
        {
            if (string.IsNullOrWhiteSpace(openId)) throw new ArgumentException("message", nameof(openId));
            var requestUser = JsonConvert.DeserializeObject<RequestUser>(userInfo.Replace("\\", ""));
            var wxappUser = await _fsql.Select<User>().Where(l => l.OpenId == openId).ToOneAsync() ?? new User();
            wxappUser.NickName = requestUser.NickName;
            wxappUser.Gender = requestUser.Gender;
            wxappUser.AvatarUrl = requestUser.AvatarUrl;
            wxappUser.Country = requestUser.Country;
            wxappUser.Province = requestUser.Province;
            wxappUser.City = requestUser.City;
            if (wxappUser.UserId > 0)
            {
                await _fsql.Update<User>().DisableGlobalFilter().SetSource(wxappUser).ExecuteAffrowsAsync();
                return wxappUser.UserId;
            }
            wxappUser.OpenId = openId;
            return await _fsql.Insert<User>().AppendData(wxappUser).ExecuteIdentityAsync();
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="wxappId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<SnsResult> WxLogin(uint wxappId, string code)
        {
            var wxapp = await _fsql.Select<Wxapp>().Where(l => l.WxappId == wxappId).ToOneAsync();
            if (wxapp == null)
                throw new Exception("请到 [客户-小程序设置] 填写正确的 siteroot 和 uniacid");
            if (string.IsNullOrEmpty(wxapp.AppId) || string.IsNullOrEmpty(wxapp.AppSecret))
                throw new Exception("请到 [后台-小程序设置] 填写appid 和 appsecret");
            return await Code2Session(code, wxapp.AppId, wxapp.AppSecret);
        }

        /// <summary>
        ///   根据登录凭证获取Sns信息（openid、session_key、unionid）
        /// </summary>
        /// <param name="code">登录时获取的 code</param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        private async Task<SnsResult> Code2Session(string code, string appId, string appSecret)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("message", nameof(code));
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={code}&grant_type=authorization_code";
            return await _wxHttpClient.GetAsync<SnsResult>(url);
        }

        /// <summary>
        /// 生成用户认证的token
        /// </summary>
        /// <param name="wxappId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        private string WxToken(uint wxappId, string openId)
        {
            return $"{wxappId}_{DateTime.Now.ConvertToTimeStamp()}_{openId}_{Guid.NewGuid()}_{GlobalConfig.EncryptKey}".GetMd5();
        }
    }
}