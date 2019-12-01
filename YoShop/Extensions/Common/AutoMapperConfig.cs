using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using YoShop.Models;

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
                //                m.CreateMap<user, UserDto>()
                //                    .ForMember(dst => dst.CreateTime,  opt => {  opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                //                    .ForMember(dst => dst.UpdateTime,  opt => {  opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });
                //                m.CreateMap<goods, GoodsDto>()
                //                    .ForMember(dst => dst.CreateTime,  opt => {  opt.MapFrom(src => src.CreateTime.ConvertToDateTime()); })
                //                    .ForMember(dst => dst.UpdateTime,  opt => {  opt.MapFrom(src => src.UpdateTime.ConvertToDateTime()); });
            });
        }
    }
}
