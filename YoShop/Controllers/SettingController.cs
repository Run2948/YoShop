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

namespace YoShop.Controllers
{
    public class SettingController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public SettingController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 商城设置

        /// <summary>
        /// 商城设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/store")]
        public async Task<IActionResult> Store()
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Store).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);
            return View(model);
        }

        /// <summary>
        /// 保存商城设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/store"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Store(string name, string is_notice, string notice)
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Store).ToLower()).ToOneAsync();

            var model = JObject.Parse(setting.Values);
            model["name"] = name;
            model["is_notice"] = is_notice;
            model["notice"] = notice;

            try
            {
                await _fsql.Update<Setting>().Set(s => s.Values,  model.ObjectToJson())
                    .Set(s => s.UpdateTime, DateTimeExtensions.GetCurrentTimeStamp())
                    .Where(s => s.Key == setting.Key).ExecuteAffrowsAsync();
                //初始化系统设置参数
                var settings = await _fsql.Select<Setting>().ToListAsync();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }

        #endregion

        #region 交易设置
        /// <summary>
        /// 交易设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/trade")]
        public async Task<IActionResult> Trade()
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Trade).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);
            return View(model);
        }

        /// <summary>
        /// 保存交易设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/trade"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Trade(string close_days, string receive_days, string refund_days, string freight_rule)
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Trade).ToLower()).ToOneAsync();

            var model = JObject.Parse(setting.Values);
            model["order"]["close_days"] = close_days;
            model["order"]["receive_days"] = receive_days;
            model["order"]["refund_days"] = refund_days;
            model["freight_rule"] = freight_rule;

            try
            {
                await _fsql.Update<Setting>().Set(s => s.Values,  model.ObjectToJson())
                    .Set(s => s.UpdateTime, DateTimeExtensions.GetCurrentTimeStamp())
                    .Where(s => s.Key == setting.Key).ExecuteAffrowsAsync();
                //初始化系统设置参数
                var settings = await _fsql.Select<Setting>().ToListAsync();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }
        #endregion

        #region 短信设置
        /// <summary>
        /// 短信设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/sms")]
        public async Task<IActionResult> Sms()
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Sms).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);
            return View(model);
        }

        /// <summary>
        /// 短信设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/sms"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Sms(string sms_default, string sms_aliyun_AccessKeyId, string sms_aliyun_AccessKeySecret, string sms_aliyun_sign, string sms_aliyun_order_pay_is_enable)
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Sms).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);

            if ("aliyun".Equals(sms_default))
            {
                model["engine"]["aliyun"]["AccessKeyId"] = sms_aliyun_AccessKeyId;
                model["engine"]["aliyun"]["AccessKeySecret"] = sms_aliyun_AccessKeySecret;
                model["engine"]["aliyun"]["sign"] = sms_aliyun_sign;
                model["engine"]["aliyun"]["order_pay"]["is_enable"] = sms_aliyun_order_pay_is_enable;
            }

            try
            {
                await _fsql.Update<Setting>().Set(s => s.Values,  model.ObjectToJson())
                    .Set(s => s.UpdateTime, DateTimeExtensions.GetCurrentTimeStamp())
                    .Where(s => s.Key == setting.Key).ExecuteAffrowsAsync();
                //初始化系统设置参数
                var settings = await _fsql.Select<Setting>().ToListAsync();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }

        /// <summary>
        /// 短信测试
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/smstest"), ValidateAntiForgeryToken]
        public async Task<IActionResult> SmsTest(string AccessKeyId, string AccessKeySecret, string sign, string msg_type, string template_code, string accept_phone)
        {
            if (string.IsNullOrEmpty(AccessKeyId))
                return No("请填写 AccessKeyId");
            if (string.IsNullOrEmpty(AccessKeySecret))
                return No("请填写 AccessKeySecret");
            if (string.IsNullOrEmpty(sign))
                return No("请填写 短信签名");
            if (string.IsNullOrEmpty(template_code))
                return No("请填写 请填写 模板ID");
            if (string.IsNullOrEmpty(accept_phone))
                return No("请填写 接收手机号");
            return Yes("发送成功");
        }

        #endregion

        #region 上传设置
        /// <summary>
        /// 上传设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/storage")]
        public async Task<IActionResult> Storage()
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Storage).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);
            return View(model);
        }

        /// <summary>
        /// 保存上传设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/storage")]
        public async Task<IActionResult> Storage(string storage_default, string qiniu_bucket, string qiniu_access_key, string qiniu_secret_key, string qiniu_domain)
        {
            var setting = await _fsql.Select<Setting>().Where(l => l.Key == nameof(StoreSetting.Storage).ToLower()).ToOneAsync();
            var model = JObject.Parse(setting.Values);

            if ("qiniu".Equals(storage_default))
            {
                model["default"] = "qiniu";
                model["engine"]["qiniu"]["bucket"] = qiniu_bucket;
                model["engine"]["qiniu"]["access_key"] = qiniu_access_key;
                model["engine"]["qiniu"]["secret_key"] = qiniu_secret_key;
                model["engine"]["qiniu"]["domain"] = qiniu_domain;
            }
            else
            {
                model["default"] = "local";
            }

            try
            {
                await _fsql.Update<Setting>().Set(s => s.Values,  model.ObjectToJson())
                    .Set(s => s.UpdateTime, DateTimeExtensions.GetCurrentTimeStamp())
                    .Where(s => s.Key == setting.Key).ExecuteAffrowsAsync();
                //初始化系统设置参数
                var settings = await _fsql.Select<Setting>().ToListAsync();
                GlobalConfig.SystemSettings = settings.ToDictionary(s => s.Key, s => JObject.Parse(s.Values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }
        #endregion

        #region 其他设置

        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.cache/clear")]
        public async Task<IActionResult> CacheClear()
        {
            return View();
        }

        /// <summary>
        /// 环境检测
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.science/index")]
        public async Task<IActionResult> ScienceIndex()
        {
            return View();
        }

        #endregion
    }
}