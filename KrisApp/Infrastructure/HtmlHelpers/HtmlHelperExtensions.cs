using KrisApp.Models.Nav;
using System.Web.Mvc;
using System.Web.Routing;

namespace KrisApp.Infrastructure.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Zwraca link wypełniony danymi z przekazanego modelu 
        /// </summary>
        public static MvcHtmlString MenuItemLink(this HtmlHelper html, MenuItemModel model)
        {
            RouteValueDictionary routeValues = null;

            if (!string.IsNullOrWhiteSpace(model.Parameter))
            {
                routeValues = new RouteValueDictionary();
                routeValues.Add("id", model.Parameter);
            }

            string targetUrl = UrlHelper.GenerateUrl("Default", model.Action, model.Controller, routeValues,
                RouteTable.Routes, html.ViewContext.RequestContext, false);

            TagBuilder anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", targetUrl);

            anchorBuilder.InnerHtml = model.Text;

            return new MvcHtmlString(anchorBuilder.ToString(TagRenderMode.Normal));
        }
    }
}