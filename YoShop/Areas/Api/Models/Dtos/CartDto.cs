using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YoShop.Models;

namespace YoShop.Areas.Api.Models
{
    public class CartDto
    {
        [JsonProperty("address")]
        public UserAddress Address { get; set; }

        [JsonProperty("exist_address")]
        public bool ExistAddress => Address != null;

        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("exist_address")]
        public bool HasError => !string.IsNullOrEmpty(ErrorMsg);

        [JsonProperty("express_price")]
        public double ExpressPrice { get; set; }

        [JsonProperty("goods_list")]
        public List<Goods> GoodsList { get; set; }

        [JsonProperty("intra_region")]
        public bool IntraRegion { get; set; } = true;

        [JsonProperty("order_total_num")]
        public int OrderTotalNum { get; set; }

        [JsonProperty("order_total_price")]
        public double OrderTotalPrice { get; set; }

        [JsonProperty("order_pay_price")]
        public string OrderPayPrice => $"{(OrderTotalPrice + ExpressPrice):N2}";

    }
}
