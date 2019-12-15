using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods_image")]
    public partial class GoodsImage : WxappEntity
    {
        [JsonProperty("id"), Column(Name = "id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint Id { get; set; }

        [JsonProperty("goods_id"), Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty("image_id"), Column(Name = "image_id", DbType = "int(11) unsigned")]
        public uint ImageId { get; set; }

        [Navigate(nameof(ImageId)),JsonProperty("file")]
        public virtual UploadFile UploadFile { get; set; }
    }
}
