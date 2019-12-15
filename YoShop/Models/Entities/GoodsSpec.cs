using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods_spec")]
    public partial class GoodsSpec : WxappEntity
    {
        [JsonProperty("goods_spec_id"), Column(Name = "goods_spec_id", DbType = "int(11) unsigned", IsIdentity = true,IsPrimary = true)]
        public uint GoodsSpecId { get; set; }

        [JsonProperty("goods_id"), Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty("goods_no"), Column(Name = "goods_no", DbType = "varchar(100)")]
        public string GoodsNo { get; set; }

        [JsonProperty("goods_price"), Column(Name = "goods_price", DbType = "decimal(10,2) unsigned")]
        public decimal GoodsPrice { get; set; }

        [JsonProperty("goods_sales"), Column(Name = "goods_sales", DbType = "int(11) unsigned")]
        public uint GoodsSales { get; set; }

        [JsonProperty("goods_weight"), Column(Name = "goods_weight", DbType = "double unsigned")]
        public double GoodsWeight { get; set; }

        [JsonProperty("line_price"), Column(Name = "line_price", DbType = "decimal(10,2) unsigned")]
        public decimal LinePrice { get; set; }

        [JsonProperty("spec_sku_id"), Column(Name = "spec_sku_id")]
        public string SpecSkuId { get; set; }

        [JsonProperty("stock_num"), Column(Name = "stock_num", DbType = "int(11) unsigned")]
        public uint StockNum { get; set; }
    }
}
