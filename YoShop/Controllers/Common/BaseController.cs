using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YoShop.Models;

namespace YoShop.Controllers
{
    /// <summary>
    /// 基础公共控制器
    /// </summary>
    public class BaseController : Controller
    {
        #region 通用返回JsonResult的封装

        /// <summary>
        /// 返回响应状态、消息、数据对象和跳转地址
        /// </summary>
        /// <param name="code">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult Build(int code, string msg, object data = null, string url = null)
        {
            var js = new ResultInfo(code, msg, data, url);
            return Build(js);
        }

        /// <summary>
        /// 返回响应状态、消息、数据和跳转地址
        /// </summary>
        /// <param name="result">ResultInfo实体</param>
        /// <returns></returns>
        protected ContentResult Build(ResultInfo result)
        {
            var js = new ResultInfo(result.Code, result.Msg, result.Data, result.Url);
            return Content(JsonConvert.SerializeObject(js, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            }), "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 返回成功状态、消息、数据对象和跳转地址
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult Yes(string msg = "Sucess", object data = null, string url = null)
        {
            return Build(code: 1, msg: msg, data: data, url: url);
        }

        /// <summary>
        /// 返回成功状态、消息、数据对象
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ContentResult YesResult(string msg, object data)
        {
            return Yes(msg: msg, data: data);
        }

        /// <summary>
        /// 返回成功状态、消息、数据对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ContentResult YesResult(object data)
        {
            return Yes(msg: "Success", data: data);
        }

        /// <summary>
        /// 返回成功状态、消息和跳转地址
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult YesRedirect(string msg, string url)
        {
            return Yes(msg: msg, url: url);
        }

        /// <summary>
        /// 返回成功状态、消息和跳转地址
        /// </summary>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult YesRedirect(string url)
        {
            return Yes(msg: "Success", url: url);
        }

        /// <summary>
        /// 返回失败状态、消息、数据对象和跳转地址
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult No(string msg = "Failure", object data = null, string url = null)
        {
            return Build(code: 0, msg: msg, data: data, url: url);
        }

        /// <summary>
        /// 返回失败状态、消息和跳转地址
        /// </summary>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        protected ContentResult NoRedirect(string url)
        {
            return Build(code: 0, msg: "Failure", url: url);
        }

        #endregion

        #region 通用返回ResultInfo的封装
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ResultInfo InfoResp(int code, string msg, object data = null, string url = null)
        {
            return new ResultInfo(code, msg, data, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected ResultInfo SuccResp(string msg, object data = null)
        {
            return InfoResp(1, msg, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected ResultInfo SuccResp(object data)
        {
            return InfoResp(1, "Success", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ResultInfo SuccResp(string msg, string url)
        {
            return InfoResp(1, msg, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected ResultInfo FailResp(string msg, string url = null)
        {
            return InfoResp(0, msg, url);
        }

        #endregion

        #region 跳转自定义错误页面
        /// <summary>
        /// 404错误页面
        /// </summary>
        /// <returns></returns>
        protected IActionResult Error() => RedirectToAction("Index", "Error");
        /// <summary>
        /// 参数错误页面
        /// </summary>
        /// <returns></returns>
        protected IActionResult ParamsError() => RedirectToAction("ParamsError", "Error");
        /// <summary>
        /// 已删除或不存在
        /// </summary>
        /// <returns></returns>
        protected IActionResult NoOrDeleted() => RedirectToAction("NoOrDeleted", "Error");
        #endregion
    }
}