using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "order_address")]
    public partial class OrderAddress : WxappEntity
    {
        [JsonProperty, Column(Name = "order_address_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint OrderAddressId { get; set; }

        [JsonProperty, Column(Name = "name", DbType = "varchar(30)")]
        public string Name { get; set; }

        [JsonProperty, Column(Name = "order_id", DbType = "int(11) unsigned")]
        public uint OrderId { get; set; }

        [JsonProperty, Column(Name = "phone", DbType = "varchar(20)")]
        public string Phone { get; set; }

        [JsonProperty, Column(Name = "province_id", DbType = "int(11) unsigned")]
        public uint ProvinceId { get; set; }

        [Navigate(nameof(ProvinceId))]
        public virtual Region Province { get; set; }

        [JsonProperty, Column(Name = "city_id", DbType = "int(11) unsigned")]
        public uint CityId { get; set; }

        [Navigate(nameof(CityId))]
        public virtual Region City { get; set; }

        [JsonProperty, Column(Name = "region_id", DbType = "int(11) unsigned")]
        public uint RegionId { get; set; }

        [Navigate(nameof(RegionId))]
        public virtual Region Region { get; set; }

        [JsonProperty, Column(Name = "detail")]
        public string Detail { get; set; }

        [JsonProperty, Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }
    }
}
