using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoShop.Controllers
{
    /// <summary>
    /// 错误处理控制器
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// AccessNoRight
        /// </summary>
        /// <returns></returns>
        [Route("AccessNoRight")]
        public IActionResult AccessNoRight()
        {
            //Response.StatusCode = 401;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 401,
                Success = false,
                Message = "没有权限！"
            });
        }

        /// <summary>
        /// PageNotFound
        /// </summary>
        /// <returns></returns>
        [Route("PageNotFound")]
        public IActionResult PageNotFound()
        {
            //Response.StatusCode = 404;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 404,
                Success = false,
                Message = "页面未找到！"
            });
        }

        /// <summary>
        /// ServerError
        /// </summary>
        /// <returns></returns>
        [Route("ServerError")]
        public IActionResult ServerError()
        {
            //Response.StatusCode = 500;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 500,
                Success = false,
                Message = "服务器内部错误！"
            });
        }

        /// <summary>
        /// ServiceUnavailable
        /// </summary>
        /// <returns></returns>
        [Route("ServiceUnavailable")]
        public IActionResult ServiceUnavailable()
        {
            //Response.StatusCode = 503;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 503,
                Success = false,
                Message = "服务不可用！"
            });
        }

        /// <summary>
        /// 自定义错误页面1
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var Message = "系统异常";
            if (Request.Method.ToLower().Equals("get"))
            {
                return View("~/Views/Error/Index.html", Message);
            }
            return Json(new
            {
                code = 0,
                msg = "Error",
                data = Message
            });
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public IActionResult ParamsError()
        {
            var Message = "请求参数异常";
            if (Request.Method.ToLower().Equals("get"))
            {
                return View("~/Views/Error/ParamsError.cshtml", Message);
            }
            return Json(new
            {
                code = 0,
                msg = "ParamsError",
                data = Message
            });
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public IActionResult NoOrDeleted()
        {
            var Message = "请求的数据不存在或已经被删除";
            if (Request.Method.ToLower().Equals("get"))
            {
                return View("~/Views/Error/NoOrDeleted.cshtml", Message);
            }
            return Json(new
            {
                code = 0,
                msg = "NoOrDeleted",
                data = Message
            });
        }
    }
}