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
        [Description("上架")]
        OnSale = 10,
        [Description("下架")]
        OffSale = 20,
    }

    public enum SpecType
    {

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


}
