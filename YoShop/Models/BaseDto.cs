using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class WxappDto : BaseDto
    {
        public uint WxappId { get; set; }
    }

    public class BaseDto
    {
        public DateTime UpdateTime { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
