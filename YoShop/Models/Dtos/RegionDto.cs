using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class RegionDto
    {
        public uint Id { get; set; }

        public byte Level { get; set; }

        public string Name { get; set; }

        public uint Pid { get; set; }

        public List<RegionDto> City { get; set; }

        public List<RegionDto> Region { get; set; }
    }
}
