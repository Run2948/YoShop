using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoShop.Models
{
    public class CategoryDto : WxappDto
    {
        public uint CategoryId { get; set; }

        public string Name { get; set; }

        public uint ImageId { get; set; }

        public UploadFile UploadFile { get; set; }

        public uint Sort { get; set; }

        public uint ParentId { get; set; }

        public Category Parent { get; set; }
    }

    public class CategorySelectDto
    {
        public uint CategoryId { get; set; }

        public string Name { get; set; }

        public uint ParentId { get; set; }
    }
}
