using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class GoodsDto : WxappDto
    {
        public uint GoodsId { get; set; }
        public virtual List<GoodsImage> GoodsImages { get; set; }

        public string GoodsName { get; set; }

        public string Content { get; set; }

        public uint GoodsSort { get; set; }

        public GoodsStatus GoodsStatus { get; set; }

        public byte IsDelete { get; set; }

        public uint SalesActual { get; set; }

        public uint SalesInitial { get; set; }

        public SpecType SpecType { get; set; }

        public string SpecMany { get; set; }

        public DeductStockType DeductStockType { get; set; }

        public uint CategoryId { get; set; }
        public Category Category { get; set; }

        public uint DeliveryId { get; set; }

        public Delivery Delivery { get; set; }
    }
}
