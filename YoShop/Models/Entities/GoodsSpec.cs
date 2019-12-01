using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods_spec")]
    public partial class GoodsSpec : WxappEntity
    {
        [JsonProperty, Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty, Column(Name = "goods_no", DbType = "varchar(100)")]
        public string GoodsNo { get; set; }

        [JsonProperty, Column(Name = "goods_price", DbType = "decimal(10,2) unsigned")]
        public decimal GoodsPrice { get; set; }

        [JsonProperty, Column(Name = "goods_sales", DbType = "int(11) unsigned")]
        public uint GoodsSales { get; set; }

        [JsonProperty, Column(Name = "goods_spec_id", DbType = "int(11) unsigned", IsIdentity = true)]
        public uint GoodsSpecId { get; set; }

        [JsonProperty, Column(Name = "goods_weight", DbType = "double unsigned")]
        public double GoodsWeight { get; set; }

        [JsonProperty, Column(Name = "line_price", DbType = "decimal(10,2) unsigned")]
        public decimal LinePrice { get; set; }

        [JsonProperty, Column(Name = "spec_sku_id")]
        public string SpecSkuId { get; set; }

        [JsonProperty, Column(Name = "stock_num", DbType = "int(11) unsigned")]
        public uint StockNum { get; set; }
    }
}
