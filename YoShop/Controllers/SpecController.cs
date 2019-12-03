using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoShop.Controllers
{
    public class SpecController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        /// <summary>
        /// 商品规格管理
        /// </summary>
        /// <param name="fsql"></param>
        public SpecController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        [HttpGet, Route("/goods.spec/addSpec")]
        public IActionResult AddSpec()
        {
                
            return View();
        }
    }
}