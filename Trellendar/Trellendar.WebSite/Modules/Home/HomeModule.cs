using Nancy;

namespace Trellendar.WebSite.Modules.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = Index;
        }

        public dynamic Index(dynamic parameters)
        {
            return View["Index"];
        }
    }
}