using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace YoShop.Models
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "order")]
    public partial class Order : WxappEntity
    {
        [JsonProperty, Column(Name = "order_id", DbType = "int(11) unsigned", IsIdentity = true, IsPrimary = true)]
        public uint OrderId { get; set; }

        [JsonProperty, Column(Name = "order_no", DbType = "varchar(20)")]
        public string OrderNo { get; set; }

        [JsonProperty, Column(Name = "order_status")]
        public OrderStatus OrderStatus { get; set; }

        [JsonProperty, Column(Name = "pay_price", DbType = "decimal(10,2) unsigned")]
        public decimal PayPrice { get; set; }

        [JsonProperty, Column(Name = "pay_status")]
        public PayStatus PayStatus { get; set; }

        [JsonProperty, Column(Name = "pay_time", DbType = "int(11) unsigned")]
        public uint PayTime { get; set; }

        [JsonProperty, Column(Name = "receipt_status")]
        public ReceiptStatus ReceiptStatus { get; set; }

        [JsonProperty, Column(Name = "receipt_time", DbType = "int(11) unsigned")]
        public uint ReceiptTime { get; set; }

        [JsonProperty, Column(Name = "total_price", DbType = "decimal(10,2) unsigned")]
        public decimal TotalPrice { get; set; }

        [JsonProperty, Column(Name = "transaction_id", DbType = "varchar(30)")]
        public string TransactionId { get; set; }

        [JsonProperty, Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }

        [JsonProperty, Column(Name = "delivery_status")]
        public DeliveryStatus DeliveryStatus { get; set; }

        [JsonProperty, Column(Name = "delivery_time", DbType = "int(11) unsigned")]
        public uint DeliveryTime { get; set; }

        [JsonProperty, Column(Name = "express_company", DbType = "varchar(50)")]
        public string ExpressCompany { get; set; }

        [JsonProperty, Column(Name = "express_no", DbType = "varchar(50)")]
        public string ExpressNo { get; set; }

        [JsonProperty, Column(Name = "express_price", DbType = "decimal(10,2) unsigned")]
        public decimal ExpressPrice { get; set; }
    }
}
