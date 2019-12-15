using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "delivery")]
    public partial class Delivery : WxappEntity
    {
        [JsonProperty("delivery_id"), Column(Name = "delivery_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint DeliveryId { get; set; }

        [JsonProperty("name"), Column(Name = "name")]
        public string Name { get; set; }

        [JsonProperty("method"), Column(Name = "method")]
        public byte Method { get; set; }

        [JsonProperty("sort"), Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }
    }
}
