using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "spec_value")]
    public partial class SpecValue : WxappEntity
    {
        [JsonProperty, Column(Name = "spec_value_id", DbType = "int(11) unsigned", IsIdentity = true,IsPrimary = true)]
        public uint SpecValueId { get; set; }

        [JsonProperty, Column(Name = "spec_id")]
        public int SpecId { get; set; }

        [JsonProperty, Column(Name = "spec_value_desc")]
        public string SpecValueDesc { get; set; }
    }
}
