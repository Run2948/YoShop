using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "region")]
    public partial class Region
    {
        [JsonProperty, Column(Name = "id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint Id { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "code", DbType = "varchar(100)")]
        public string Code { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "first", DbType = "varchar(50)")]
        public string First { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "lat", DbType = "varchar(100)")]
        public string Lat { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "level", DbType = "tinyint(4) unsigned")]
        public byte Level { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "lng", DbType = "varchar(100)")]
        public string Lng { get; set; }

        [JsonProperty("merger_name"), Column(Name = "merger_name")]
        public string MergerName { get; set; }

        [JsonProperty, Column(Name = "name", DbType = "varchar(100)")]
        public string Name { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "pid", DbType = "int(11) unsigned")]
        public uint Pid { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "pinyin", DbType = "varchar(100)")]
        public string Pinyin { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "short_name", DbType = "varchar(100)")]
        public string ShortName { get; set; }

        [JsonProperty, JsonIgnore, Column(Name = "zip_code", DbType = "varchar(100)")]
        public string ZipCode { get; set; }
    }
}
