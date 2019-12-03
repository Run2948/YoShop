using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class DeliveryRuleDto : WxappDto
    {
        public uint RuleId { get; set; }

        public string Region { get; set; }

        public double First { get; set; }

        public decimal FirstFee { get; set; }

        public double Additional { get; set; }

        public decimal AdditionalFee { get; set; }

        public uint DeliveryId { get; set; }
    }

    public class DeliveryRuleWithRegionDto
    {
        public string Content { get; set; }

        public string Region { get; set; }

        public double First { get; set; }

        public decimal FirstFee { get; set; }

        public double Additional { get; set; }

        public decimal AdditionalFee { get; set; }
    }

}
