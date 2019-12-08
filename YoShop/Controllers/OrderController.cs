using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FreeSql;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using YoShop.Extensions;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Views;

namespace YoShop.Controllers
{
    public class OrderController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public OrderController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        #region 订单列表

        /// <summary>
        /// 待发货订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/delivery_list")]
        public async Task<IActionResult> Delivery(int? page, int? size)
        {
            var select = GetOrderList(payStatus: 20, deliveryStatus: 10);
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "待发货";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        /// <summary>
        /// 待收货订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/receipt_list")]
        public async Task<IActionResult> Receipt(int? page, int? size)
        {
            var select = GetOrderList(payStatus: 20, deliveryStatus: 20, receiptStatus: 10);
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "待收货";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        /// <summary>
        /// 待付款订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/pay_list")]
        public async Task<IActionResult> Pay(int? page, int? size)
        {
            var select = GetOrderList(payStatus: 10, orderStatus: 10);
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "待付款";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        /// <summary>
        /// 已完成订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/complete_list")]
        public async Task<IActionResult> Complete(int? page, int? size)
        {
            var select = GetOrderList(orderStatus: 30);
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "已完成";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        /// <summary>
        /// 已取消订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/cancel_list")]
        public async Task<IActionResult> Cancel(int? page, int? size)
        {
            var select = GetOrderList(orderStatus: 20);
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "已取消";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        /// <summary>
        /// 全部订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/order/all_list")]
        public async Task<IActionResult> All(int? page, int? size)
        {
            var select = GetOrderList();
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 10).ToListAsync();
            ViewData["Title"] = "全部";
            return View("Index", new OrderListViewModel(list.Mapper<List<OrderDto>>(), total));
        }

        #endregion

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("/order/detail/orderId/{id}")]
        public async Task<IActionResult> Detail(uint id)
        {
            var order = await _fsql.Select<Order>().Where(l => l.OrderId == id)
                .IncludeMany(l => l.OrderGoods, then => then.Include(i => i.GoodsImage))
                .Include(l => l.OrderAddress)
                .Include(l => l.User)
                .ToOneAsync();
            return View(order.Mapper<OrderDto>());
        }

        /// <summary>
        /// 确认发货
        /// </summary>
        /// <param name="expressNo"></param>
        /// <param name="id"></param>
        /// <param name="expressCompany"></param>
        /// <returns></returns>
        [HttpPost, Route("/order/delivery/orderId/{id}")]
        public async Task<IActionResult> Delivery(string expressCompany, string expressNo, uint id)
        {
            var order = await _fsql.Select<Order>().Where(l => l.OrderId == id).ToOneAsync();
            if (order == null) return NoOrDeleted();
            if (order.PayStatus == 10 || order.DeliveryStatus == 20)
                return No("该订单不合法");
            try
            {
                order.DeliveryStatus = 20;
                order.DeliveryTime = DateTime.Now.ConvertToTimeStamp();
                order.ExpressCompany = expressCompany;
                order.ExpressNo = expressNo;
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("发货成功！", "/order/receipt_list");
        }


        private ISelect<Order> GetOrderList(uint payStatus = 0, uint deliveryStatus = 0, uint receiptStatus = 0, uint orderStatus = 0)
        {
            var select = _fsql.Select<Order>();
            if (payStatus > 0)
                select.Where(l => l.PayStatus == payStatus);
            if (deliveryStatus > 0)
                select.Where(l => l.DeliveryStatus == deliveryStatus);
            if (receiptStatus > 0)
                select.Where(l => l.ReceiptStatus == receiptStatus);
            if (orderStatus > 0)
                select.Where(l => l.OrderStatus == orderStatus);
            return select.IncludeMany(l => l.OrderGoods, then => then.Include(i => i.GoodsImage)).Include(l => l.User).OrderByDescending(l => l.OrderId);
        }
    }
}