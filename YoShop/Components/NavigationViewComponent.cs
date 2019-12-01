using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using YoShop.Models;

namespace YoShop.Components
{
    /// <summary>
    /// 视图组件
    /// </summary>
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 视图组件
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public NavigationViewComponent(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 渲染菜单
        /// </summary>
        /// <returns></returns>
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 60 * 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IViewComponentResult Invoke()
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "json", "menus.json");
            var jsonString = File.ReadAllText(filePath);
            var menus = JsonConvert.DeserializeObject<MenuDto[]>(jsonString).ToList();
            return View(menus);
        }
    }
}
