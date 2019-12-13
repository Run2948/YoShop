using System;
using System.Collections.Generic;
using System.Text;

namespace Unit.Tests
{
    public class SpecManyDto
    {
        public SpecAttr[] SpecAttr { get; set; }
        public SpecList[] SpecList { get; set; }
    }

    public class SpecAttr
    {
        public int SpecId { get; set; }
        public string SpecName { get; set; }
        public SpecItem[] SpecItems { get; set; }
    }

    public class SpecItem
    {
        public int SpecValueId { get; set; }
        public string SpecValueName { get; set; }
    }

    public class SpecList
    {
        public string SpecSkuId { get; set; }
        public GoodsSpecRel[] GoodsSpecRels { get; set; }
        public GoodsSpec GoodsSpec { get; set; }
    }

    public class GoodsSpec
    {
        public string GoodsNo { get; set; }
        public string GoodsPrice { get; set; }
        public string LinePrice { get; set; }
        public string StockNum { get; set; }
        public string GoodsWeight { get; set; }
    }

    public class GoodsSpecRel
    {
        public int SpecValueId { get; set; }
        public string SpecValueName { get; set; }
        public int SpecId { get; set; }
    }
}
