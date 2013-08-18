using Nancy;

namespace Trellendar.WebSite.Modules.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = parameters => View["index"];
        }
    }
}