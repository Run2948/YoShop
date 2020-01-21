using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "user_address")]
    public partial class UserAddress : WxappEntity
    {
        [JsonProperty("address_id"), Column(Name = "address_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint AddressId { get; set; }

        [JsonProperty("user_id"), Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }

        [JsonProperty("name"), Column(Name = "name", DbType = "varchar(30)")]
        public string Name { get; set; }

        [JsonProperty("phone"), Column(Name = "phone", DbType = "varchar(20)")]
        public string Phone { get; set; }

        [JsonProperty("province_id"), Column(Name = "province_id", DbType = "int(11) unsigned")]
        public uint ProvinceId { get; set; }

        [JsonProperty("city_id"), Column(Name = "city_id", DbType = "int(11) unsigned")]
        public uint CityId { get; set; }

        [JsonProperty("region_id"), Column(Name = "region_id", DbType = "int(11) unsigned")]
        public uint RegionId { get; set; }

        [JsonProperty("region"), Navigate(nameof(RegionId))]
        public virtual Region Region { get; set; }

        [JsonProperty("detail"), Column(Name = "detail")]
        public string Detail { get; set; }
    }
}
