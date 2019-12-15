using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "upload_group")]
    public partial class UploadGroup : WxappEntity
    {
        [JsonProperty("group_id"), Column(Name = "group_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint GroupId { get; set; }

        [JsonProperty("group_name"), Column(Name = "group_name", DbType = "varchar(30)")]
        public string GroupName { get; set; }

        [JsonProperty("group_type"), Column(Name = "group_type", DbType = "varchar(10)")]
        public string GroupType { get; set; }

        [JsonProperty("sort"), Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }
    }
}
