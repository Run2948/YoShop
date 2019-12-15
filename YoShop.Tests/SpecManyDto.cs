using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Unit.Tests
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
        public SpecItem[] SpecItems { get; set; }
    }

    public class SpecItem
    {
        [JsonProperty("spec_id")]
        public int SpecValueId { get; set; }

        [JsonProperty("spec_value")]
        public string SpecValueName { get; set; }
    }

    public class SpecList
    {
        [JsonProperty("spec_sku_id")]
        public string SpecSkuId { get; set; }

        [JsonProperty("rows")]
        public GoodsSpecRel[] GoodsSpecRels { get; set; }

        [JsonProperty("form")]
        public GoodsSpec GoodsSpec { get; set; }
    }

    public class GoodsSpec
    {
        [JsonProperty("goods_no")]
        public string GoodsNo { get; set; }

        [JsonProperty("goods_price")]
        public decimal GoodsPrice { get; set; }
        [JsonProperty("goods_weight")]
        public double GoodsWeight { get; set; }

        [JsonProperty("line_price")]
        public decimal LinePrice { get; set; }

        [JsonProperty("stock_num")]
        public uint StockNum { get; set; }
    }

    public class GoodsSpecRel
    {
        [JsonProperty("spec_id")]
        public uint SpecId { get; set; }

        [JsonProperty("spec_value")]
        public string SpecValueName { get; set; }

        [JsonProperty("item_id")]
        public uint SpecValueId { get; set; }
    }
}
