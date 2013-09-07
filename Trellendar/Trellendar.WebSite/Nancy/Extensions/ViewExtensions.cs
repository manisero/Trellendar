using Nancy.Json;
using Nancy.ViewEngines.Razor;

namespace Trellendar.WebSite.Nancy.Extensions
{
    public static class ViewExtensions
    {
        public static void Initialize(this NancyRazorViewBase view, string title = null, string ngModule = null, string ngController = null)
        {
            view.Layout = "Layout";
            view.ViewBag.Title = title;
            view.ViewBag.NgModule = ngModule;
            view.ViewBag.NgController = ngController;
        }

        public static IHtmlString SerializeModel(this NancyRazorViewBase view)
        {
            return view.Html.Raw(new JavaScriptSerializer().Serialize(view.Model));
        }
    }
}