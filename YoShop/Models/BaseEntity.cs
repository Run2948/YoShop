using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace YoShop.Models
{
    public class WxappEntity : BaseEntity
    {
        [JsonProperty, Column(Name = "wxapp_id", DbType = "int(11) unsigned")]
        public uint WxappId { get; set; }
    }

    public class BaseEntity
    {
        [JsonProperty, Column(Name = "update_time", DbType = "int(11) unsigned")]
        public uint UpdateTime { get; set; }

        [JsonProperty, Column(Name = "create_time", DbType = "int(11) unsigned")]
        public uint CreateTime { get; set; }
    }
}
