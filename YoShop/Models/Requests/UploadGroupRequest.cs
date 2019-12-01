using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models.Requests
{
    /// <summary>
    /// 上传文件分组
    /// </summary>
    public class UploadGroupRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public uint group_id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 分组类别
        /// </summary>
        public string group_type { get; set; }
    }
}
