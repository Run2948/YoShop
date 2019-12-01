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
        [JsonProperty, Column(Name = "id", IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [JsonProperty, Column(Name = "code", DbType = "varchar(100)")]
        public string Code { get; set; }

        [JsonProperty, Column(Name = "first", DbType = "varchar(50)")]
        public string First { get; set; }

        [JsonProperty, Column(Name = "lat", DbType = "varchar(100)")]
        public string Lat { get; set; }

        [JsonProperty, Column(Name = "level", DbType = "tinyint(4) unsigned")]
        public byte? Level { get; set; }

        [JsonProperty, Column(Name = "lng", DbType = "varchar(100)")]
        public string Lng { get; set; }

        [JsonProperty, Column(Name = "merger_name")]
        public string MergerName { get; set; }

        [JsonProperty, Column(Name = "name", DbType = "varchar(100)")]
        public string Name { get; set; }

        [JsonProperty, Column(Name = "pid")]
        public int? Pid { get; set; }

        [JsonProperty, Column(Name = "pinyin", DbType = "varchar(100)")]
        public string Pinyin { get; set; }

        [JsonProperty, Column(Name = "short_name", DbType = "varchar(100)")]
        public string ShortName { get; set; }

        [JsonProperty, Column(Name = "zip_code", DbType = "varchar(100)")]
        public string ZipCode { get; set; }
    }
}
