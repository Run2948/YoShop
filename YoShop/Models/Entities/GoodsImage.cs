using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods_image")]
    public partial class GoodsImage : WxappEntity
    {
        [JsonProperty, Column(Name = "id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint Id { get; set; }

        [JsonProperty, Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty, Column(Name = "image_id")]
        public int ImageId { get; set; }


    }
}
