using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoShop.Extensions;

namespace YoShop.Models.Requests
{
    public class SellerDeliveryRequest : DeliveryDto
    {
        public string[] Region { get; set; }

        public double[] First { get; set; }

        public decimal[] FirstFee { get; set; }

        public double[] Additional { get; set; }

        public decimal[] AdditionalFee { get; set; }

        public List<DeliveryRule> BuildDeliveryRules(uint deliveryId)
        {
            return Region.Select((t, i) => new DeliveryRule()
            {
                Region = t,
                First = First[i],
                FirstFee = FirstFee[i],
                Additional = Additional[i],
                AdditionalFee = AdditionalFee[i],
                CreateTime = CreateTime.ConvertToTimeStamp(),
                UpdateTime = UpdateTime.ConvertToTimeStamp(),
                WxappId = WxappId,
                DeliveryId = deliveryId
            }).ToList();
        }

        public List<DeliveryRule> BuildDeliveryRules()
        {
            return Region.Select((t, i) => new DeliveryRule()
            {
                Region = t,
                First = First[i],
                FirstFee = FirstFee[i],
                Additional = Additional[i],
                AdditionalFee = AdditionalFee[i],
                CreateTime = CreateTime.ConvertToTimeStamp(),
                UpdateTime = UpdateTime.ConvertToTimeStamp(),
                WxappId = WxappId,
                DeliveryId = DeliveryId
            }).ToList();
        }
    }
}
