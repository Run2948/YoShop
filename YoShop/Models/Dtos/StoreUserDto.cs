using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class StoreUserDto
    {
        public uint StoreUserId { get; set; }

        public string UserName { get; set; }

        public uint WxappId { get; set; }

        public uint UpdateTime { get; set; }

        public uint CreateTime { get; set; }
    }
}
