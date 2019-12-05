using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class GoodsSpecRelDto : WxappDto
    {
        public uint Id { get; set; }

        public uint GoodsId { get; set; }

        public uint SpecId { get; set; }

        public string SpecValueName { get; set; }

        public uint SpecValueId { get; set; }
    }
}
