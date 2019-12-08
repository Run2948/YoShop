using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using YoShop.Models;

namespace YoShop
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "spec")]
    public partial class Spec : WxappEntity
    {
        [JsonProperty, Column(Name = "spec_id", DbType = "int(11) unsigned", IsIdentity = true,IsPrimary = true)]
        public uint SpecId { get; set; }

        [JsonProperty, Column(Name = "spec_name")]
        public string SpecName { get; set; }
    }
}
