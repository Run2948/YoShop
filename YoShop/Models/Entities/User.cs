using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "user")]
    public partial class User : WxappEntity
    {
        [JsonProperty("user_id"), Column(Name = "user_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint UserId { get; set; }

        [JsonProperty("address"), Navigate(nameof(UserId))]
        public virtual List<UserAddress> UserAddress { get; set; }

        [JsonProperty("open_id"), Column(Name = "open_id")]
        public string OpenId { get; set; }

        [JsonProperty("nickName"), Column(Name = "nick_name")]
        public string NickName { get; set; }

        [JsonProperty, Column(Name = "gender")]
        public byte Gender { get; set; }

        [JsonProperty, Column(Name = "avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty, Column(Name = "country", DbType = "varchar(50)")]
        public string Country { get; set; }

        [JsonProperty, Column(Name = "province", DbType = "varchar(50)")]
        public string Province { get; set; }

        [JsonProperty, Column(Name = "city", DbType = "varchar(50)")]
        public string City { get; set; }

        [JsonProperty("address_id"), Column(Name = "address_id", DbType = "int(11) unsigned")]
        public uint AddressId { get; set; }

        [JsonProperty("default_address"), Navigate(nameof(AddressId))]
        public virtual UserAddress DefaultAddress { get; set; }
    }
}
