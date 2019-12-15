using System;
using Newtonsoft.Json;

namespace Unit.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "{\"spec_attr\":[{\"group_id\":2,\"group_name\":\"颜色\",\"spec_items\":[{\"item_id\":4,\"spec_value\":\"红色\"},{\"item_id\":3,\"spec_value\":\"蓝色\"}]},{\"group_id\":3,\"group_name\":\"重量\",\"spec_items\":[{\"item_id\":5,\"spec_value\":\"1KG\"},{\"item_id\":6,\"spec_value\":\"2KG\"}]}],\"spec_list\":[{\"spec_sku_id\":\"4_5\",\"rows\":[{\"rowspan\":2,\"item_id\":4,\"spec_value\":\"红色\"},{\"rowspan\":1,\"item_id\":5,\"spec_value\":\"1KG\"}],\"form\":{\"goods_no\":\"11\",\"goods_price\":\"10\",\"line_price\":\"12\",\"stock_num\":\"100\",\"goods_weight\":\"1\"}},{\"spec_sku_id\":\"4_6\",\"rows\":[{\"rowspan\":1,\"item_id\":6,\"spec_value\":\"2KG\"}],\"form\":{\"goods_no\":\"12\",\"goods_price\":\"15\",\"line_price\":\"17\",\"stock_num\":\"90\",\"goods_weight\":\"1\"}},{\"spec_sku_id\":\"3_5\",\"rows\":[{\"rowspan\":2,\"item_id\":3,\"spec_value\":\"蓝色\"},{\"rowspan\":1,\"item_id\":5,\"spec_value\":\"1KG\"}],\"form\":{\"goods_no\":\"21\",\"goods_price\":\"20\",\"line_price\":\"22\",\"stock_num\":\"120\",\"goods_weight\":\"1\"}},{\"spec_sku_id\":\"3_6\",\"rows\":[{\"rowspan\":1,\"item_id\":6,\"spec_value\":\"2KG\"}],\"form\":{\"goods_no\":\"22\",\"goods_price\":\"25\",\"line_price\":\"17\",\"stock_num\":\"60\",\"goods_weight\":\"1\"}}]}";
            var specManyDto = JsonConvert.DeserializeObject<SpecManyDto>(json);
            Console.WriteLine("OK!");
        }
    }
}
