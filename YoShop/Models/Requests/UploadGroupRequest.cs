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
        public uint GroupId { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 分组类别
        /// </summary>
        public string GroupType { get; set; }
    }
}
