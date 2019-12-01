using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class MenuDto
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string index { get; set; }
        public MenuDto[] submenu { get; set; }
        public string color { get; set; }
        public bool is_svg { get; set; }
        public string[] uris { get; set; }
        public bool active { get; set; }
    }
}
