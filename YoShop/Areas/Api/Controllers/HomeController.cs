using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YoShop.Areas.Api.Controllers
{
    public class HomeController : ApiBaseController
    {
        public IActionResult Index()
        {
            return Yes("api接口层");
        }
    }
}