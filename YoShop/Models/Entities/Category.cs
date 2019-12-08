using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn),Table(Name = "category")]
    public partial class Category : WxappEntity
    {
        [JsonProperty,Column(Name = "category_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint CategoryId { get; set; }

        [JsonProperty,Column(Name = "name", DbType = "varchar(50)")]
        public string Name { get; set; }

        [JsonProperty,Column(Name = "image_id", DbType = "int(11) unsigned")]
        public uint ImageId { get; set; }

        [Navigate(nameof(ImageId))]
        public virtual UploadFile CategoryImage { get; set; }

        [JsonProperty,Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }

        [JsonProperty,Column(Name = "parent_id", DbType = "int(11) unsigned")]
        public uint ParentId { get; set; }
        public virtual Category Parent { get; set; }
    }
}

