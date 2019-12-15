using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "wxapp_help")]
    public partial class WxappHelp : WxappEntity
    {
        [JsonProperty("help_id"), Column(Name = "help_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint HelpId { get; set; }

        [JsonProperty("content"), Column(Name = "content", DbType = "text")]
        public string Content { get; set; }

        [JsonProperty("sort"), Column(Name = "sort", DbType = "int(11) unsigned")]
        public uint Sort { get; set; }

        [JsonProperty("title"), Column(Name = "title")]
        public string Title { get; set; }
    }
}
