using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using YoShop.Models;

namespace YoShop
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "user_address")]
    public partial class UserAddress : WxappEntity
    {
        [JsonProperty, Column(Name = "address_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint AddressId { get; set; }

        [JsonProperty, Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }

        [JsonProperty, Column(Name = "name", DbType = "varchar(30)")]
        public string Name { get; set; }

        [JsonProperty, Column(Name = "phone", DbType = "varchar(20)")]
        public string Phone { get; set; }

        [JsonProperty, Column(Name = "province_id", DbType = "int(11) unsigned")]
        public uint ProvinceId { get; set; }

        [JsonProperty, Column(Name = "city_id", DbType = "int(11) unsigned")]
        public uint CityId { get; set; }

        [JsonProperty, Column(Name = "region_id", DbType = "int(11) unsigned")]
        public uint RegionId { get; set; }

        [JsonProperty, Column(Name = "detail")]
        public string Detail { get; set; }
    }
}
