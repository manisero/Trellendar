using Nancy.ViewEngines.Razor;

namespace Trellendar.WebSite.Nancy.Extensions
{
    public static class ViewExtensions
    {
        public static void Initialize(this NancyRazorViewBase view, string title = null, string ngController = null)
        {
            view.Layout = "Layout";
            view.ViewBag.Title = title;
            view.ViewBag.NgController = ngController;
        }
    }
}