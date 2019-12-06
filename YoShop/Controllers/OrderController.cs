using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoShop.Controllers
{
    public class OrderController : SellerBaseController
    {
        [HttpGet,Route("/order/delivery_list")]
        public IActionResult Index()
        {
            return View();
        }
    }
}