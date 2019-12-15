using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "upload_file")]
    public partial class UploadFile : WxappEntity
    {
        [JsonProperty("file_id"), Column(Name = "file_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint FileId { get; set; }

        [JsonProperty("file_name"), Column(Name = "file_name")]
        public string FileName { get; set; }

        [JsonProperty("file_size"), Column(Name = "file_size", DbType = "int(11) unsigned")]
        public uint FileSize { get; set; }

        [JsonProperty("file_type"), Column(Name = "file_type", DbType = "varchar(20)")]
        public string FileType { get; set; }

        [JsonProperty("extension"), Column(Name = "extension", DbType = "varchar(20)")]
        public string Extension { get; set; }

        [JsonProperty("file_url"), Column(Name = "file_url")]
        public string FileUrl { get; set; }

        [JsonProperty("group_id"), Column(Name = "group_id", DbType = "int(11) unsigned")]
        public uint GroupId { get; set; }

        [Navigate(nameof(GroupId))]
        public virtual UploadGroup UploadGroup { get; set; }

        [JsonProperty("is_delete"), Column(Name = "is_delete", DbType = "tinyint(1) unsigned")]
        public byte IsDelete { get; set; }

        [JsonProperty("storage"), Column(Name = "storage", DbType = "varchar(20)")]
        public string Storage { get; set; }
    }
}
