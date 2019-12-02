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
}
