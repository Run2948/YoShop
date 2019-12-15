using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YoShop.Models
{
    public class GoodsSpecDto : WxappDto
    {
        public uint GoodsSpecId { get; set; }

        public uint GoodsId { get; set; }

        [JsonProperty("goods_no")]
        public string GoodsNo { get; set; }

        [JsonProperty("goods_price")]
        public decimal GoodsPrice { get; set; }

        public uint GoodsSales { get; set; }

        [JsonProperty("goods_weight")]
        public double GoodsWeight { get; set; }

        [JsonProperty("line_price")]
        public decimal LinePrice { get; set; }

        public string SpecSkuId { get; set; }

        [JsonProperty("stock_num")]
        public uint StockNum { get; set; }
    }
}
