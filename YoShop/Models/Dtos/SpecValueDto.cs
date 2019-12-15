using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YoShop.Models
{
    public class SpecValueDto : WxappDto
    {
        [JsonProperty("spec_id")]
        public int SpecValueId { get; set; }

        [JsonProperty("spec_value")]
        public string SpecValueName { get; set; }

        [JsonIgnore]
        public uint SpecId { get; set; }
    }
}
