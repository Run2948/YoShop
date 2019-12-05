using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class GoodsSpecDto : WxappDto
    {
        public uint GoodsId { get; set; }

        public string GoodsNo { get; set; }

        public decimal GoodsPrice { get; set; }

        public uint GoodsSales { get; set; }

        public uint GoodsSpecId { get; set; }

        public double GoodsWeight { get; set; }

        public decimal LinePrice { get; set; }

        public string SpecSkuId { get; set; }

        public uint StockNum { get; set; }
    }
}
