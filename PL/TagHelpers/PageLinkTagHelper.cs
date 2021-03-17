using System;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PL.Models;

namespace PL.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // набор ссылок будет представлять список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            // формируем три ссылки - на текущую, предыдущую и следующую
            TagBuilder currentItem = CreateTag(PageModel.CurrentPage, urlHelper);

            // создаем ссылку на предыдущую страницу, если она есть
            if (PageModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(PageModel.CurrentPage - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }

            tag.InnerHtml.AppendHtml(currentItem);
            // создаем ссылку на следующую страницу, если она есть
            if (PageModel.HasNextPage)
            {
                TagBuilder nextItem = CreateTag(PageModel.CurrentPage + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }

   //     public TagBuilder PageLinks(this HtmlHelper html,
   //                                   PageViewModel pageViewModel,
   //                                   Func<int, string> pageUrl)
   //     {
   //         TagBuilder result = new TagBuilder("li");
			//for (int i = 1; i <= pageViewModel.TotalPages; i++)
			//{
			//	TagBuilder tag = new TagBuilder("li");
			//	tag.MergeAttribute("href", pageUrl(i));
			//	tag.InnerHtml.Append(i.ToString());
			//	if (i == pageViewModel.CurrentPage)
			//	{
			//		tag.AddCssClass("selected");
			//		tag.AddCssClass("btn-primary");
			//	}
			//	tag.AddCssClass("btn btn-default");
			//	result.InnerHtml.AppendLine(tag.ToString());
			//}
			//return result;
   //     }

        TagBuilder CreateTag(int currentPage, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (currentPage == this.PageModel.CurrentPage)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = currentPage });
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(currentPage.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
