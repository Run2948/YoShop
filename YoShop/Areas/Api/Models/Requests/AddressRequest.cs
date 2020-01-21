using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Areas.Api.Models.Requests
{
    public class AddressRequest
    {
        public uint AddressId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Region { get; set; }

        public string Detail { get; set; }

        public uint WxappId { get; set; }
    }
}
