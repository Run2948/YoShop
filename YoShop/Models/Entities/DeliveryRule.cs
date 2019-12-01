using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "delivery_rule")]
    public partial class DeliveryRule : WxappEntity
    {
        [JsonProperty, Column(Name = "rule_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint RuleId { get; set; }

        [JsonProperty, Column(Name = "region", DbType = "text")]
        public string Region { get; set; }

        [JsonProperty, Column(Name = "first", DbType = "double unsigned")]
        public double First { get; set; }

        [JsonProperty, Column(Name = "first_fee", DbType = "decimal(10,2) unsigned")]
        public decimal FirstFee { get; set; }

        [JsonProperty, Column(Name = "additional", DbType = "double unsigned")]
        public double Additional { get; set; }

        [JsonProperty, Column(Name = "additional_fee", DbType = "decimal(10,2) unsigned")]
        public decimal AdditionalFee { get; set; }

        [JsonProperty, Column(Name = "delivery_id", DbType = "int(11) unsigned")]
        public uint DeliveryId { get; set; }
    }
}
