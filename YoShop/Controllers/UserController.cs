using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoShop.Extensions.Common;
using YoShop.Models;
using YoShop.Models.Views;

namespace YoShop.Controllers
{
    public class UserController : SellerBaseController
    {
        private readonly IFreeSql _fsql;

        public UserController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/user/index")]
        public async Task<IActionResult> Index(int? page, int? size)
        {
            var select = _fsql.Select<User>();
            var total = await select.CountAsync();
            var list = await select.Page(page ?? 1, size ?? 15).OrderBy(u => u.UserId).ToListAsync();
            return View(new UserListViewModel(list.Mapper<List<UserDto>>(), total));
        }
    }
}
