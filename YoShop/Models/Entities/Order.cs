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

        [Navigate(nameof(OrderId))]
        public virtual List<OrderGoods> OrderGoods { get; set; }

        [Navigate(nameof(OrderId))]
        public virtual OrderAddress OrderAddress { get; set; }

        [JsonProperty, Column(Name = "order_no", DbType = "varchar(20)")]
        public string OrderNo { get; set; }

        [JsonProperty, Column(Name = "order_status")]
        public byte OrderStatus { get; set; }

        [JsonProperty, Column(Name = "pay_price", DbType = "decimal(10,2) unsigned")]
        public decimal PayPrice { get; set; }

        [JsonProperty, Column(Name = "pay_status")]
        public byte PayStatus { get; set; }

        [JsonProperty, Column(Name = "pay_time", DbType = "int(11) unsigned")]
        public uint PayTime { get; set; }

        [JsonProperty, Column(Name = "receipt_status")]
        public byte ReceiptStatus { get; set; }

        [JsonProperty, Column(Name = "receipt_time", DbType = "int(11) unsigned")]
        public uint ReceiptTime { get; set; }

        [JsonProperty, Column(Name = "total_price", DbType = "decimal(10,2) unsigned")]
        public decimal TotalPrice { get; set; }

        [JsonProperty, Column(Name = "transaction_id", DbType = "varchar(30)")]
        public string TransactionId { get; set; }

        [JsonProperty, Column(Name = "user_id", DbType = "int(11) unsigned")]
        public uint UserId { get; set; }

        [Navigate(nameof(UserId))]
        public virtual User User { get; set; }

        [JsonProperty, Column(Name = "delivery_status")]
        public byte DeliveryStatus { get; set; }

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
