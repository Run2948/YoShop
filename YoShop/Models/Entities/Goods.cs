using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "goods")]
    public partial class Goods : WxappEntity
    {
        [JsonProperty("goods_id"), Column(Name = "goods_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint GoodsId { get; set; }

        [Navigate(nameof(GoodsId)), JsonProperty("image")]
        public virtual List<GoodsImage> GoodsImages { get; set; }

        [Navigate(nameof(GoodsId)), JsonProperty("spec")]
        public virtual List<GoodsSpec> GoodsSpecs { get; set; }

        [JsonProperty("goods_name"), Column(Name = "goods_name")]
        public string GoodsName { get; set; }

        [JsonProperty("content"), Column(Name = "content", DbType = "longtext")]
        public string Content { get; set; }

        [JsonProperty("goods_sort"), Column(Name = "goods_sort", DbType = "int(11) unsigned")]
        public uint GoodsSort { get; set; }

        [JsonProperty("goods_status"), Column(Name = "goods_status")]
        public byte GoodsStatus { get; set; }

        [JsonProperty("is_delete"), Column(Name = "is_delete", DbType = "tinyint(1) unsigned")]
        public byte IsDelete { get; set; }

        [JsonProperty("goods_sales"), Column(Name = "sales_actual", DbType = "int(11) unsigned")]
        public uint SalesActual { get; set; }

        [JsonProperty("sales_initial"), JsonIgnore, Column(Name = "sales_initial", DbType = "int(11) unsigned")]
        public uint SalesInitial { get; set; }

        [JsonProperty("spec_type"), Column(Name = "spec_type")]
        public byte SpecType { get; set; }

        [JsonIgnore, Column(Name = "spec_many", DbType = "text")]
        public string SpecMany { get; set; }

        [JsonProperty("deduct_stock_type"), Column(Name = "deduct_stock_type")]
        public byte DeductStockType { get; set; }

        [JsonProperty("category_id"), Column(Name = "category_id", DbType = "int(11) unsigned")]
        public uint CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [JsonProperty("delivery_id"), Column(Name = "delivery_id", DbType = "int(11) unsigned")]
        public uint DeliveryId { get; set; }

        [JsonProperty("delivery")]
        public virtual Delivery Delivery { get; set; }
    }
}
