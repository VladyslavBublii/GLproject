using System;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PL.Models;

namespace PL.Helpers
{
	public static class PagingHelpers
	{

		//public static MvcHtmlString PageLinks(this HtmlHelper html,
		//							  PagingInfo pagingInfo,
		//							  Func<int, string> pageUrl)
		//{
		//	StringBuilder result = new StringBuilder();
		//	for (int i = 1; i <= pagingInfo.TotalPages; i++)
		//	{
		//		TagBuilder tag = new TagBuilder("a");
		//		tag.MergeAttribute("href", pageUrl(i));
		//		tag.InnerHtml = i.ToString();
		//		if (i == pagingInfo.CurrentPage)
		//		{
		//			tag.AddCssClass("selected");
		//			tag.AddCssClass("btn-primary");
		//		}
		//		tag.AddCssClass("btn btn-default");
		//		result.Append(tag.ToString());
		//	}
		//	return MvcHtmlString.Create(result.ToString());

		public static TagBuilder PageLinks(this HtmlHelper html,
							  PageViewModel pageViewModel,
							  Func<int, string> pageUrl)
		{
			TagBuilder result = new TagBuilder("a");
			for (int i = 1; i <= pageViewModel.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a");
				tag.MergeAttribute("href", pageUrl(i));
				tag.InnerHtml.Append(i.ToString());
				if (i == pageViewModel.CurrentPage)
				{
					tag.AddCssClass("selected");
					tag.AddCssClass("btn-primary");
				}
				tag.AddCssClass("btn btn-default");
				result.InnerHtml.AppendLine(tag.ToString());
			}
			return result;
		}
	}
}
