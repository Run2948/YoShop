using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class DeliveryDto : WxappDto
    {
        public uint DeliveryId { get; set; }

        public string Name { get; set; }

        public DeliveryMethod Method { get; set; }

        public uint Sort { get; set; }

    }

    public class DeliveryWithRuleDto : DeliveryDto
    {
        public string[] Region { get; set; }

        public double[] First { get; set; }

        public decimal[] FirstFee { get; set; }

        public double[] Additional { get; set; }

        public decimal[] AdditionalFee { get; set; }

        public List<DeliveryRuleDto> BuildDeliveryRuleDto(uint deliveryId)
        {
            return Region.Select((t, i) => new DeliveryRuleDto()
            {
                Region = t,
                First = First[i],
                FirstFee = FirstFee[i],
                Additional = Additional[i],
                AdditionalFee = AdditionalFee[i],
                CreateTime = CreateTime,
                UpdateTime = UpdateTime,
                WxappId = WxappId,
                DeliveryId = deliveryId
            }).ToList();
        }

        public List<DeliveryRuleDto> BuildDeliveryRuleDto()
        {
            return Region.Select((t, i) => new DeliveryRuleDto()
            {
                Region = t,
                First = First[i],
                FirstFee = FirstFee[i],
                Additional = Additional[i],
                AdditionalFee = AdditionalFee[i],
                CreateTime = CreateTime,
                UpdateTime = UpdateTime,
                WxappId = WxappId,
                DeliveryId = DeliveryId
            }).ToList();
        }
    }

    public class DeliverySelectDto
    {
        public uint DeliveryId { get; set; }

        public string Name { get; set; }
    }
}
