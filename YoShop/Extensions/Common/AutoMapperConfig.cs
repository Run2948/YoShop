using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using YoShop.Models;
using YoShop.Models.Requests;

namespace YoShop.Extensions.Common
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<StoreUser, StoreUserDto>();
                m.CreateMap<Category, CategoryDto>()
                    //映射发生之前
                    //                    .BeforeMap((src, dst) => { dst.CreateTime = src.CreateTime.ConvertToDateTime(); })
                    //                    .BeforeMap((src, dst) => { dst.UpdateTime = src.UpdateTime.ConvertToDateTime(); })
                    //映射发生之后
                    .AfterMap((src, dst) => { dst.CreateTime = src.CreateTime.ConvertToDateTime(); })
                    .AfterMap((src, dst) => { dst.UpdateTime = src.UpdateTime.ConvertToDateTime(); });
                m.CreateMap<CategoryDto, Category>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<User, UserDto>()
                .ForMember(dst => dst.Gender, opt => { opt.MapFrom(src => src.Gender.ToEnum<Gender>()); })
                .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<Delivery, DeliveryDto>()
                    .ForMember(dst => dst.Method, opt => { opt.MapFrom(src => src.Method.ToEnum<DeliveryMethod>()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<DeliveryDto, Delivery>()
                    .ForMember(dst => dst.Method, opt => { opt.MapFrom(src => src.Method.ToByte()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<SellerDeliveryRequest, DeliveryDto>();

                m.CreateMap<DeliveryRule, DeliveryRuleDto>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<DeliveryRuleDto, DeliveryRule>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<Goods, GoodsDto>()
                    .ForMember(dst => dst.GoodsStatus, opt => { opt.MapFrom(src => src.GoodsStatus.ToEnum<GoodsStatus>()); })
                    .ForMember(dst => dst.SpecType, opt => { opt.MapFrom(src => src.SpecType.ToEnum<SpecType>()); })
                    .ForMember(dst => dst.DeductStockType, opt => { opt.MapFrom(src => src.DeductStockType.ToEnum<DeductStockType>()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<GoodsDto, Goods>()
                    .ForMember(dst => dst.GoodsStatus, opt => { opt.MapFrom(src => src.GoodsStatus.ToByte()); })
                    .ForMember(dst => dst.SpecType, opt => { opt.MapFrom(src => src.SpecType.ToByte()); })
                    .ForMember(dst => dst.DeductStockType, opt => { opt.MapFrom(src => src.DeductStockType.ToByte()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<SellerGoodsRequest, Goods>()
                    .ForMember(dst => dst.GoodsStatus, opt => { opt.MapFrom(src => src.GoodsStatus.ToByte()); })
                    .ForMember(dst => dst.SpecType, opt => { opt.MapFrom(src => src.SpecType.ToByte()); })
                    .ForMember(dst => dst.DeductStockType, opt => { opt.MapFrom(src => src.DeductStockType.ToByte()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<Goods, SellerGoodsRequest>()
                    .ForMember(dst => dst.GoodsStatus, opt => { opt.MapFrom(src => src.GoodsStatus.ToEnum<GoodsStatus>()); })
                    .ForMember(dst => dst.SpecType, opt => { opt.MapFrom(src => src.SpecType.ToEnum<SpecType>()); })
                    .ForMember(dst => dst.DeductStockType, opt => { opt.MapFrom(src => src.DeductStockType.ToEnum<DeductStockType>()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<GoodsSpec, GoodsSpecDto>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<GoodsSpecDto, GoodsSpec>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<GoodsSpecRel, GoodsSpecRelDto>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<GoodsSpecRelDto, GoodsSpecRel>()
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<Order, OrderDto>()
                    .ForMember(dst => dst.OrderStatus, opt => { opt.MapFrom(src => src.OrderStatus.ToEnum<OrderStatus>()); })
                    .ForMember(dst => dst.PayStatus, opt => { opt.MapFrom(src => src.PayStatus.ToEnum<PayStatus>()); })
                    .ForMember(dst => dst.ReceiptStatus, opt => { opt.MapFrom(src => src.ReceiptStatus.ToEnum<ReceiptStatus>()); })
                    .ForMember(dst => dst.DeliveryStatus, opt => { opt.MapFrom(src => src.DeliveryStatus.ToEnum<DeliveryStatus>()); })
                    .ForMember(dst => dst.PayTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.ReceiptTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.DeliveryTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });

                m.CreateMap<OrderDto, Order>()
                    .ForMember(dst => dst.OrderStatus, opt => { opt.MapFrom(src => src.OrderStatus.ToByte()); })
                    .ForMember(dst => dst.PayStatus, opt => { opt.MapFrom(src => src.PayStatus.ToByte()); })
                    .ForMember(dst => dst.ReceiptStatus, opt => { opt.MapFrom(src => src.ReceiptStatus.ToByte()); })
                    .ForMember(dst => dst.DeliveryStatus, opt => { opt.MapFrom(src => src.DeliveryStatus.ToByte()); })
                    .ForMember(dst => dst.PayTime, opt => { opt.MapFrom(src => src.PayTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.ReceiptTime, opt => { opt.MapFrom(src => src.ReceiptTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.DeliveryTime, opt => { opt.MapFrom(src => src.DeliveryTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.CreateTime, opt => { opt.MapFrom(src => src.CreateTime.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.UpdateTime, opt => { opt.MapFrom(src => src.UpdateTime.ConvertToTimeStamp()); });

                m.CreateMap<Wxapp, WxappConfig>();
            });
        }
    }
}
