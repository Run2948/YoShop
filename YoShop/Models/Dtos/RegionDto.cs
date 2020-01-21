using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

    public class AddressRegionDto
    {
        public string Province { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
    }
}
