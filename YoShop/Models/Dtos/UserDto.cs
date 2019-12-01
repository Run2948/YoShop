using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class UserDto : WxappDto
    {
        public uint UserId { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public Gender Gender { get; set; }

        public string AvatarUrl { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public uint AddressId { get; set; }

        public UserAddress UserAddress { get; set; }
    }
}
