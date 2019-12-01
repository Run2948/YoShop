using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "delivery")]
    public partial class Delivery : WxappEntity
    {
        [JsonProperty, Column(Name = "delivery_id", DbType = "int(11) unsigned", IsIdentity = true)]
        public uint DeliveryId { get; set; }

        [JsonProperty, Column(Name = "name")]
        public string Name { get; set; }
        
        [JsonProperty, Column(Name = "method")]
        public byte Method { get; set; }

        [JsonProperty, Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }
    }
}
