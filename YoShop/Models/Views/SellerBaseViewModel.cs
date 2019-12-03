using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models.Views
{
    public class SellerBaseViewModel<T> where T : WxappDto
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long Total { get; set; } = 0;

        public SellerBaseViewModel()
        {

        }

        public SellerBaseViewModel(List<T> list, long total)
        {
            List = list;
            Total = total;
        }
    }

    public class UserListViewModel : SellerBaseViewModel<UserDto>
    {
        public UserListViewModel(List<UserDto> list, long total)
        : base(list, total)
        {

        }
    }

    public class DeliveryListViewModel : SellerBaseViewModel<DeliveryDto>
    {
        public DeliveryListViewModel(List<DeliveryDto> list, long total)
            : base(list, total)
        {

        }
    }

    public class GoodsListViewModel : SellerBaseViewModel<GoodsDto>
    {
        public GoodsListViewModel(List<GoodsDto> list, long total)
            : base(list, total)
        {

        }
    }
}
