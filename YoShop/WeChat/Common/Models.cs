using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YoShop.WeChat.Common
{
    /// <summary>
    ///     API请求结果
    /// </summary>
    public class WxResult
    {
        /// <summary>
        ///     返回码
        /// </summary>
        [JsonProperty("errcode")]
        public virtual ReturnCode ReturnCode { get; set; }

        /// <summary>
        ///     错误消息
        /// </summary>
        [JsonProperty("errmsg")]
        public virtual string Message { get; set; }

        /// <summary>
        ///     请求详情（一般请忽略）
        /// </summary>
        public virtual string DetailResult { get; set; }

        /// <summary>
        ///     是否为成功返回
        /// </summary>
        /// <returns></returns>
        public virtual bool IsSuccess()
        {
            return ReturnCode == ReturnCode.请求成功;
        }

        /// <summary>
        ///     获取友好提示
        /// </summary>
        /// <returns></returns>
        public virtual string GetFriendlyMessage()
        {
            return ReturnCode.ToString();
        }
    }

    /// <summary>
    ///     公众号返回码（JSON）
    /// </summary>
    public enum ReturnCode
    {
        请求成功 = 0,
        系统繁忙_此时请开发者稍候再试 = -1,
        AppSecret错误或者AppSecret不属于这个小程序_请开发者确认AppSecret的正确性 = 40001,
        请确保grant_type字段值为client_credential = 40002,
        不合法的AppID_请开发者检查AppID的正确性 = 40013,
        授权Code不正确 = 40029,
        template_id不正确 = 40037,
        form_id不正确或过期 = 41028,
        form_id已被使用 = 41029,
        page不正确 = 41030,
        接口调用超过限额_目前默认每个帐号日调用限额为100万 = 45009
    }
}
