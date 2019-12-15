using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YoShop.Models
{
    public class SpecManyDto
    {
        [JsonProperty("spec_attr")]
        public SpecAttr[] SpecAttr { get; set; }

        [JsonProperty("spec_list")]
        public SpecList[] SpecList { get; set; }
    }

    public class SpecAttr
    {
        [JsonProperty("group_id")]
        public uint SpecId { get; set; }

        [JsonProperty("group_name")]
        public string SpecName { get; set; }

        [JsonProperty("spec_items")]
        public SpecValueDto[] SpecItems { get; set; }
    }

    public class SpecList
    {
        [JsonProperty("spec_sku_id")]
        public string SpecSkuId { get; set; }

        [JsonProperty("rows")]
        public GoodsSpecRelDto[] GoodsSpecRels { get; set; }

        [JsonProperty("form")]
        public GoodsSpecDto GoodsSpec { get; set; }
    }
}
