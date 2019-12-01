using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), FreeSql.DataAnnotations.Table(Name = "category")]
    public partial class Category : WxappEntity
    {
        [JsonProperty, FreeSql.DataAnnotations.Column(Name = "category_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint CategoryId { get; set; }

        [JsonProperty, FreeSql.DataAnnotations.Column(Name = "name", DbType = "varchar(50)")]
        public string Name { get; set; }

        [JsonProperty, FreeSql.DataAnnotations.Column(Name = "image_id", DbType = "int(11) unsigned")]
        public uint ImageId { get; set; }

        [Navigate("ImageId")]
        public virtual UploadFile UploadFile { get; set; }

        [JsonProperty, FreeSql.DataAnnotations.Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }

        [JsonProperty, FreeSql.DataAnnotations.Column(Name = "parent_id", DbType = "int(11) unsigned")]
        public uint ParentId { get; set; }
        public virtual Category Parent { get; set; }
    }
}

