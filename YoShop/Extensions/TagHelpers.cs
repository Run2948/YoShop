using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YoShop.Extensions
{
    /// <summary>
    /// 分页TagHelper帮助类
    /// </summary>
    [HtmlTargetElement("quick-pager")]
    /*
     * <ul class="pagination">
     *   <li><a href="/index.php?s=/store/user/index?page=1">&laquo;</a></li>
     *   <li><a href="/index.php?s=/store/user/index?page=1">1</a></li>
     *   <li class="active"><span>2</span></li>
     *   <li class="disabled"><span>&raquo;</span></li>
     * </ul> 
     */
    public partial class QuickPagerTagHelper : TagHelper
    {
        /// <summary>
        /// 视图上下文
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [HtmlAttributeName("total-count")]
        public long TotalCount { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; } = 15;
        /// <summary>
        /// 请求地址
        /// </summary>
        [HtmlAttributeName("route-url")]
        public string RouteUrl { get; set; }

        /// <summary>
        /// 渲染分页条码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");

            if (TotalCount == 0) return;

            if (string.IsNullOrEmpty(RouteUrl)) 
                RouteUrl = $"/{ViewContext.RouteData.Values["Controller"].ToString().ToLower()}/{ViewContext.RouteData.Values["Action"].ToString().ToLower()}";

            //总页数
            var totalPage = (int)Math.Ceiling(TotalCount * 1.0f / PageSize * 1.0f);

            //当前页
            var currentPage = 1;
            var querys = ViewContext.HttpContext.Request.Query;
            if (querys.Any() && querys.ContainsKey("page"))
                currentPage = Convert.ToInt32(querys["page"][0]);

            //分页条码
            var sb = new StringBuilder(string.Empty);

            #region 构造分页样式

            sb.AppendFormat("    <li class=\"{1}\"><a href=\"{0}\">&laquo;</a></li>", currentPage == 1 ? "javascript:void(0);" : $"{RouteUrl}?page=1", currentPage == 1 ? "disabled" : "");
            for (int i = 1; i <= totalPage; i++)
                sb.AppendFormat("    <li class=\"{1}\"><a href=\"{0}\">{2}</a></li>", currentPage == i ? "javascript:void(0);" : $"{RouteUrl}?page={i}", currentPage == i ? "active" : "", i);
            sb.AppendFormat("    <li class=\"{1}\"><a href=\"{0}\">&raquo;</a></li>", currentPage == totalPage ? "javascript:void(0);" : $"{RouteUrl}?page={totalPage}", currentPage == totalPage ? "disabled" : "");

            #endregion

            //output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.AppendHtml(sb.ToString());

            await base.ProcessAsync(context, output);
        }

    }


}
