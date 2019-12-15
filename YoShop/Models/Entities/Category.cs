using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "category")]
    public partial class Category : WxappEntity
    {
        [JsonProperty("category_id"), Column(Name = "category_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint CategoryId { get; set; }

        [Navigate(nameof(CategoryId)),JsonProperty("child")]
        public virtual List<Category> Child { get; set; }

        [JsonProperty("name"), Column(Name = "name", DbType = "varchar(50)")]
        public string Name { get; set; }

        [JsonIgnore, Column(Name = "image_id", DbType = "int(11) unsigned")]
        public uint ImageId { get; set; }

        [Navigate(nameof(ImageId)),JsonProperty("image")]
        public virtual UploadFile Image { get; set; }

        [JsonProperty("sort"), Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }

        [JsonProperty("parent_id"), Column(Name = "parent_id", DbType = "int(11) unsigned")]
        public uint ParentId { get; set; }
        public virtual Category Parent { get; set; }
    }
}

