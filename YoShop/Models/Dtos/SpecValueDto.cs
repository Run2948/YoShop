using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class SpecValueDto : WxappDto
    {
        public uint SpecValueId { get; set; }

        public string SpecValueName { get; set; }

        public uint SpecId { get; set; }
    }
}
