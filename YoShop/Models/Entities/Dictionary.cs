using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "dictionary")]
    public partial class Dictionary
    {
        [JsonProperty, Column(Name = "id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint Id { get; set; }

        [JsonProperty, Column(Name = "name")]
        public string Name { get; set; }

        [JsonProperty, Column(Name = "type", DbType = "varchar(30)")]
        public string Type { get; set; }

        [JsonProperty, Column(Name = "value")]
        public string Value { get; set; }
    }
}
