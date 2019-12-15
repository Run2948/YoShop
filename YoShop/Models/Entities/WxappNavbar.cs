using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "wxapp_navbar")]
    public partial class WxappNavbar : WxappEntity
    {
        [JsonProperty("navbar_id"), Column(Name = "navbar_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint NavbarId { get; set; }

        [JsonProperty("wxapp_title"), Column(Name = "wxapp_title", DbType = "varchar(100)")]
        public string WxappTitle { get; set; }

        [JsonProperty("top_text_color"), Column(Name = "top_text_color")]
        public byte TopTextColor { get; set; }

        [JsonProperty("top_background_color"), Column(Name = "top_background_color", DbType = "varchar(10)")]
        public string TopBackgroundColor { get; set; }
    }
}
