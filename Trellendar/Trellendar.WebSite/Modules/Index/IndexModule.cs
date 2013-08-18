using Nancy;

namespace Trellendar.WebSite.Modules.Index
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index"];
        }
    }
}