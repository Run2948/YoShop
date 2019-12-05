using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class SpecManyDto
    {
        public SpecAttr[] SpecAttr { get; set; }
        public SpecList[] SpecList { get; set; }
    }

    public class SpecAttr
    {
        public uint SpecId { get; set; }
        public string SpecName { get; set; }
        public SpecValueDto[] SpecItems { get; set; }
    }

    public class SpecList
    {
        public string SpecSkuId { get; set; }
        public GoodsSpecRelDto[] GoodsSpecRels { get; set; }
        public GoodsSpecDto GoodsSpec { get; set; }
    }
}
