using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class WxappHelpDto : WxappDto
    {
        public uint HelpId { get; set; }

        public string Content { get; set; }

        public uint Sort { get; set; }

        public string Title { get; set; }
    }
}
