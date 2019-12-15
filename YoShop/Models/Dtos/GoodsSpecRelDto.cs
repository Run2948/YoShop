using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YoShop.Models
{
    public class GoodsSpecRelDto : WxappDto
    {
        public uint Id { get; set; }

        public uint GoodsId { get; set; }

        [JsonProperty("spec_id")]
        public uint SpecId { get; set; }

        [JsonProperty("spec_value")]
        public string SpecValueName { get; set; }

        [JsonProperty("item_id")]
        public uint SpecValueId { get; set; }
    }
}
