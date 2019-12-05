using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Css.Values;
using Newtonsoft.Json;
using YoShop.Extensions;
using YoShop.Extensions.Common;

namespace YoShop.Models.Requests
{
    public class SellerGoodsRequest : WxappDto
    {
        public uint GoodsId { get; set; }

        public string GoodsName { get; set; }

        public string Content { get; set; }

        public uint GoodsSort { get; set; }

        public GoodsStatus GoodsStatus { get; set; }

        public byte IsDelete { get; set; }

        public uint SalesActual { get; set; }

        public uint SalesInitial { get; set; }

        public SpecType SpecType { get; set; }

        public DeductStockType DeductStockType { get; set; }

        public uint CategoryId { get; set; }

        public uint DeliveryId { get; set; }

        public uint[] ImageIds { get; set; }

        public GoodsSpecDto GoodsSpec { get; set; }

        public string SpecMany { get; set; }

        public List<GoodsImage> BuildGoodsImages(uint goodsId)
        {
            List<GoodsImage> list = null;
            if (ImageIds != null && ImageIds.Any())
            {
                list = new List<GoodsImage>();
                foreach (var id in ImageIds)
                {
                    list.Add(new GoodsImage()
                    {
                        ImageId = id,
                        GoodsId = goodsId,
                        CreateTime = CreateTime.ConvertToTimeStamp(),
                        UpdateTime = UpdateTime.ConvertToTimeStamp(),
                        WxappId = WxappId
                    });
                }
            }
            return list;
        }

        public List<GoodsImage> BuildGoodsImages()
        {
            List<GoodsImage> list = null;
            if (ImageIds != null && ImageIds.Any())
            {
                list = new List<GoodsImage>();
                foreach (var id in ImageIds)
                {
                    list.Add(new GoodsImage()
                    {
                        ImageId = id,
                        GoodsId = GoodsId,
                        CreateTime = CreateTime.ConvertToTimeStamp(),
                        UpdateTime = UpdateTime.ConvertToTimeStamp(),
                        WxappId = WxappId
                    });
                }
            }
            return list;
        }

        public GoodsSpec BuildGoodsSpec(uint goodsId)
        {
            GoodsSpec.CreateTime = CreateTime;
            GoodsSpec.UpdateTime = UpdateTime;
            GoodsSpec.WxappId = WxappId;

            GoodsSpec.GoodsId = goodsId;
            GoodsSpec.SpecSkuId = null;
            return GoodsSpec.Mapper<GoodsSpec>();
        }

        public GoodsSpec BuildGoodsSpec()
        {
            GoodsSpec.CreateTime = CreateTime;
            GoodsSpec.UpdateTime = UpdateTime;
            GoodsSpec.WxappId = WxappId;

            GoodsSpec.GoodsId = GoodsId;
            GoodsSpec.SpecSkuId = null;
            return GoodsSpec.Mapper<GoodsSpec>();
        }

        public List<GoodsSpec> BuildGoodsSpecs(uint goodsId)
        {
            var list = new List<GoodsSpec>();
            var specMany = JsonConvert.DeserializeObject<SpecManyDto>(SpecMany);
            foreach (var specList in specMany.SpecList)
            {
                specList.GoodsSpec.GoodsId = goodsId;
                specList.GoodsSpec.CreateTime = CreateTime;
                specList.GoodsSpec.UpdateTime = UpdateTime;
                specList.GoodsSpec.WxappId = WxappId;
                var goodsSpec = specList.GoodsSpec.Mapper<GoodsSpec>();
                list.Add(goodsSpec);
            }
            return list;
        }

        public List<GoodsSpec> BuildGoodsSpecs()
        {
            var list = new List<GoodsSpec>();
            var specMany = JsonConvert.DeserializeObject<SpecManyDto>(SpecMany);
            foreach (var specList in specMany.SpecList)
            {
                specList.GoodsSpec.GoodsId = GoodsId;
                specList.GoodsSpec.CreateTime = CreateTime;
                specList.GoodsSpec.UpdateTime = UpdateTime;
                specList.GoodsSpec.WxappId = WxappId;
                var goodsSpec = specList.GoodsSpec.Mapper<GoodsSpec>();
                list.Add(goodsSpec);
            }
            return list;
        }

        public List<GoodsSpecRel> BuildGoodsSpecRels(uint goodsId)
        {
            var list = new List<GoodsSpecRel>();
            var specMany = JsonConvert.DeserializeObject<SpecManyDto>(SpecMany);
            foreach (var specList in specMany.SpecList)
            {
                foreach (var goodsSpecRelDto in specList.GoodsSpecRels)
                {
                    goodsSpecRelDto.GoodsId = goodsId;
                    goodsSpecRelDto.CreateTime = CreateTime;
                    goodsSpecRelDto.UpdateTime = UpdateTime;
                    goodsSpecRelDto.WxappId = WxappId;
                    var goodsSpecRel = goodsSpecRelDto.Mapper<GoodsSpecRel>();
                    list.Add(goodsSpecRel);
                }
            }
            return list;
        }

        public List<GoodsSpecRel> BuildGoodsSpecRels()
        {
            var list = new List<GoodsSpecRel>();
            var specMany = JsonConvert.DeserializeObject<SpecManyDto>(SpecMany);
            foreach (var specList in specMany.SpecList)
            {
                foreach (var goodsSpecRelDto in specList.GoodsSpecRels)
                {
                    goodsSpecRelDto.GoodsId = GoodsId;
                    goodsSpecRelDto.CreateTime = CreateTime;
                    goodsSpecRelDto.UpdateTime = UpdateTime;
                    goodsSpecRelDto.WxappId = WxappId;
                    var goodsSpecRel = goodsSpecRelDto.Mapper<GoodsSpecRel>();
                    list.Add(goodsSpecRel);
                }
            }
            return list;
        }
    }
}
