using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoShop.Controllers
{
    public class WxAppController : Controller
    {
        [HttpGet, Route("/wxapp/setting")]
        public IActionResult Setting()
        {
            return View();
        }
    }
}