using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "store_user")]
    public partial class StoreUser : WxappEntity
    {
        [JsonProperty, Column(Name = "store_user_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint StoreUserId { get; set; }

        [JsonProperty, Column(Name = "user_name")]
        public string UserName { get; set; }

        [JsonProperty, Column(Name = "password")]
        public string Password { get; set; }
    }
}
