using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "upload_file")]
    public partial class UploadFile : WxappEntity
    {
        [JsonProperty, Column(Name = "file_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint FileId { get; set; }

        [JsonProperty, Column(Name = "file_name")]
        public string FileName { get; set; }

        [JsonProperty, Column(Name = "file_size", DbType = "int(11) unsigned")]
        public uint FileSize { get; set; }

        [JsonProperty, Column(Name = "file_type", DbType = "varchar(20)")]
        public string FileType { get; set; }

        [JsonProperty, Column(Name = "extension", DbType = "varchar(20)")]
        public string Extension { get; set; }

        [JsonProperty, Column(Name = "file_url")]
        public string FileUrl { get; set; }

        [JsonProperty, Column(Name = "group_id", DbType = "int(11) unsigned")]
        public uint GroupId { get; set; }

        [Navigate("GroupId")]
        public virtual UploadGroup UploadGroup { get; set; }

        [JsonProperty, Column(Name = "is_delete")]
        public byte IsDelete { get; set; }

        [JsonProperty, Column(Name = "storage", DbType = "varchar(20)")]
        public string Storage { get; set; }
    }
}
