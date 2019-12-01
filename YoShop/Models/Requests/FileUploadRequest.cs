using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models.Requests
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUploadRequest
    {
        /// <summary>
        /// 上传Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 上传文件名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 文件分组
        /// </summary>
        public uint group_id { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime lastModifiedDate { get; set; }
        /// <summary>
        /// 上传文件大小
        /// </summary>
        public uint size { get; set; }
    }
}
