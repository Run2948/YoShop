using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods")]
    public partial class Goods : WxappEntity
    {
        [JsonProperty, Column(Name = "goods_id", DbType = "int(11) unsigned", IsIdentity = true,IsPrimary = true)]
        public uint GoodsId { get; set; }

        [JsonProperty, Column(Name = "goods_name")]
        public string GoodsName { get; set; }

        [JsonProperty, Column(Name = "content", DbType = "longtext")]
        public string Content { get; set; }

        [JsonProperty, Column(Name = "goods_sort", DbType = "int(11) unsigned")]
        public uint GoodsSort { get; set; }

        [JsonProperty, Column(Name = "goods_status")]
        public byte GoodsStatus { get; set; }

        [JsonProperty, Column(Name = "is_delete")]
        public byte IsDelete { get; set; }

        [JsonProperty, Column(Name = "sales_actual", DbType = "int(11) unsigned")]
        public uint SalesActual { get; set; }

        [JsonProperty, Column(Name = "sales_initial", DbType = "int(11) unsigned")]
        public uint SalesInitial { get; set; }

        [JsonProperty, Column(Name = "spec_type")]
        public byte SpecType { get; set; }

        [JsonProperty, Column(Name = "deduct_stock_type")]
        public byte DeductStockType { get; set; }

        [JsonProperty, Column(Name = "category_id", DbType = "int(11) unsigned")]
        public uint CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [JsonProperty, Column(Name = "delivery_id", DbType = "int(11) unsigned")]
        public uint DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}
