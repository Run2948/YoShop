using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "order_goods")]
    public partial class OrderGoods : WxappEntity
    {
        [JsonProperty, Column(Name = "order_goods_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint OrderGoodsId { get; set; }

        [JsonProperty, Column(Name = "goods_id", DbType = "int(11) unsigned")]
        public uint GoodsId { get; set; }

        [JsonProperty, Column(Name = "goods_name")]
        public string GoodsName { get; set; }

        [JsonProperty, Column(Name = "goods_no", DbType = "varchar(100)")]
        public string GoodsNo { get; set; }

        [JsonProperty, Column(Name = "goods_price", DbType = "decimal(10,2) unsigned")]
        public decimal GoodsPrice { get; set; }

        [JsonProperty, Column(Name = "goods_spec_id", DbType = "int(11) unsigned")]
        public uint GoodsSpecId { get; set; }

        [JsonProperty, Column(Name = "goods_weight", DbType = "double unsigned")]
        public double GoodsWeight { get; set; }

        [JsonProperty, Column(Name = "image_id", DbType = "int(11) unsigned")]
        public uint ImageId { get; set; }

        [JsonProperty, Column(Name = "line_price", DbType = "decimal(10,2) unsigned")]
        public decimal LinePrice { get; set; }

        [JsonProperty, Column(Name = "content", DbType = "longtext")]
        public string Content { get; set; }

        [JsonProperty, Column(Name = "deduct_stock_type")]
        public byte DeductStockType { get; set; }

        [JsonProperty, Column(Name = "goods_attr", DbType = "varchar(500)")]
        public string GoodsAttr { get; set; }

        [JsonProperty, Column(Name = "order_id", DbType = "int(11) unsigned")]
        public uint OrderId { get; set; }

        [JsonProperty, Column(Name = "spec_sku_id")]
        public string SpecSkuId { get; set; }

        [JsonProperty, Column(Name = "spec_type")]
        public byte SpecType { get; set; }

        [JsonProperty, Column(Name = "total_num", DbType = "int(11) unsigned")]
        public uint TotalNum { get; set; }

        [JsonProperty, Column(Name = "total_price", DbType = "decimal(10,2) unsigned")]
        public decimal TotalPrice { get; set; }

        [JsonProperty, Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }
    }
}
