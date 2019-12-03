using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public enum StoreSetting
    {
        /// <summary>
        /// 短信通知
        /// </summary>
        [Description("短信通知")]
        Sms,
        /// <summary>
        /// 上传设置
        /// </summary>
        [Description("上传设置")]
        Storage,
        /// <summary>
        /// 商城设置
        /// </summary>
        [Description("商城设置")]
        Store,
        /// <summary>
        /// 交易设置
        /// </summary>
        [Description("交易设置")]
        Trade
    }

    public enum GoodsStatus
    {
        上架 = 10,
        下架 = 20,
    }

    public enum Gender
    {
        未知 = 0,
        男 = 1,
        女 = 2
    }

    public enum SpecType
    {
        单规格 = 10,
        多规格 = 20
    }

    public enum DeductStockType
    {
        下单减库存 = 10,
        付款减库存 = 20
    }

    public enum OrderStatus
    {

    }

    public enum PayStatus
    {

    }

    public enum ReceiptStatus
    {

    }

    public enum DeliveryStatus
    {

    }

    public enum DeliveryMethod
    {
        按件数 = 10,
        按重量 = 20,
    }
}
