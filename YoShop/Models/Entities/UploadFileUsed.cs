using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "upload_file_used")]
    public partial class UploadFileUsed : WxappEntity
    {
        [JsonProperty, Column(Name = "file_id", DbType = "int(11) unsigned")]
        public uint FileId { get; set; }

        [JsonProperty, Column(Name = "from_id", DbType = "int(11) unsigned")]
        public uint FromId { get; set; }

        [JsonProperty, Column(Name = "from_type", DbType = "varchar(20)")]
        public string FromType { get; set; }

        [JsonProperty, Column(Name = "used_id", DbType = "int(11) unsigned", IsIdentity = true)]
        public uint UsedId { get; set; }
    }
}
