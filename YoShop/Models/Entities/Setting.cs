using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "setting")]
    public partial class Setting : WxappEntity
    {
        [JsonProperty, Column(Name = "key", DbType = "varchar(30)")]
        public string Key { get; set; }

        [JsonProperty, Column(Name = "values", DbType = "mediumtext")]
        public string Values { get; set; }

        [JsonProperty, Column(Name = "describe")]
        public string Describe { get; set; }
    }
}
