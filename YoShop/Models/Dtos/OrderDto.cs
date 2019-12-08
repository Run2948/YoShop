using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class OrderDto : WxappDto
    {
        public uint OrderId { get; set; }

        public List<OrderGoods> OrderGoods { get; set; }

        public OrderAddress OrderAddress { get; set; }

        public string OrderNo { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal PayPrice { get; set; }

        public PayStatus PayStatus { get; set; }

        public DateTime PayTime { get; set; }

        public ReceiptStatus ReceiptStatus { get; set; }

        public DateTime ReceiptTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string TransactionId { get; set; }

        public uint UserId { get; set; }

        public User User { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string ExpressCompany { get; set; }

        public string ExpressNo { get; set; }

        public decimal ExpressPrice { get; set; }
    }
}
