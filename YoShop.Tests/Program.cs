using System;
using Newtonsoft.Json;

namespace Unit.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
//            string json = "{\"specAttr\":[{\"specId\":2,\"specName\":\"颜色\",\"specItems\":[{\"specValueId\":3,\"specValueName\":\"蓝色\"},{\"specValueId\":7,\"specValueName\":\"紫色\"}]},{\"specId\":4,\"specName\":\"净含量\",\"specItems\":[{\"specValueId\":8,\"specValueName\":\"10ml\"},{\"specValueId\":9,\"specValueName\":\"20ml\"}]}],\"specList\":[{\"specSkuId\":\"3_8\",\"goodsSpecRels\":[{\"rowSpan\":2,\"specId\":2,\"specValueId\":3,\"specValueName\":\"蓝色\"},{\"rowSpan\":1,\"specId\":4,\"specValueId\":8,\"specValueName\":\"10ml\"}],\"goodsSpec\":{\"goodsNo\":\"21\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"3_9\",\"goodsSpecRels\":[{\"rowSpan\":1,\"specId\":4,\"specValueId\":9,\"specValueName\":\"20ml\"}],\"goodsSpec\":{\"goodsNo\":\"22\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"7_8\",\"goodsSpecRels\":[{\"rowSpan\":2,\"specId\":2,\"specValueId\":7,\"specValueName\":\"紫色\"},{\"rowSpan\":1,\"specId\":4,\"specValueId\":8,\"specValueName\":\"10ml\"}],\"goodsSpec\":{\"goodsNo\":\"23\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"7_9\",\"goodsSpecRels\":[{\"rowSpan\":1,\"specId\":4,\"specValueId\":9,\"specValueName\":\"20ml\"}],\"goodsSpec\":{\"goodsNo\":\"24\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}}]}";
            string json2 = "[{\"specSkuId\":\"3_8\",\"goodsSpecRels\":[{\"rowSpan\":2,\"specId\":2,\"specValueId\":3,\"specValueName\":\"蓝色\"},{\"rowSpan\":1,\"specId\":4,\"specValueId\":8,\"specValueName\":\"10ml\"}],\"goodsSpec\":{\"goodsNo\":\"21\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"3_9\",\"goodsSpecRels\":[{\"rowSpan\":1,\"specId\":4,\"specValueId\":9,\"specValueName\":\"20ml\"}],\"goodsSpec\":{\"goodsNo\":\"22\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"7_8\",\"goodsSpecRels\":[{\"rowSpan\":2,\"specId\":2,\"specValueId\":7,\"specValueName\":\"紫色\"},{\"rowSpan\":1,\"specId\":4,\"specValueId\":8,\"specValueName\":\"10ml\"}],\"goodsSpec\":{\"goodsNo\":\"23\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}},{\"specSkuId\":\"7_9\",\"goodsSpecRels\":[{\"rowSpan\":1,\"specId\":4,\"specValueId\":9,\"specValueName\":\"20ml\"}],\"goodsSpec\":{\"goodsNo\":\"24\",\"goodsPrice\":\"1\",\"linePrice\":\"1\",\"stockNum\":\"90\",\"goodsWeight\":\"1\"}}]";
//            var specManyDto = JsonConvert.DeserializeObject<SpecManyDto>(json);
            var specManyDto = JsonConvert.DeserializeObject<SpecList[]>(json2);

            Console.WriteLine("Hello World!");
        }
    }
}
