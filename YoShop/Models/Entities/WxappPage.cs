using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using YoShop.Models;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "wxapp_page")]
    public partial class WxappPage : WxappEntity
    {
        [JsonProperty, Column(Name = "page_id", DbType = "int(11) unsigned", IsIdentity = true,IsPrimary = true)]
        public uint PageId { get; set; }

        [JsonProperty, Column(Name = "page_data", DbType = "longtext")]
        public string PageData { get; set; }

        [JsonProperty, Column(Name = "page_type")]
        public byte PageType { get; set; }
    }
}
