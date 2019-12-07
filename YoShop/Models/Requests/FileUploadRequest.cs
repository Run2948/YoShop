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
        public string Id { get; set; }
        /// <summary>
        /// 上传文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 文件分组
        /// </summary>
        public uint GroupId { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
        /// <summary>
        /// 上传文件大小
        /// </summary>
        public uint Size { get; set; }
    }
}
