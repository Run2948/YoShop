using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class ResultInfo
    {
        public ResultInfo(int code, string msg, object data, string url)
        {
            Code = code;
            Msg = msg;
            Data = data;
            Url = url;
        }

        public ResultInfo(int code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }

        public ResultInfo(int code, string msg, string url)
        {
            Code = code;
            Msg = msg;
            Url = url;
        }

        public ResultInfo(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        public ResultInfo()
        {
        }

        /// <summary>
        /// 返回的状态码：ok: 1, error: 0, timeout: 2
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回的提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 需要跳转的地址
        /// </summary>
        public string Url { get; set; }
    }
}
