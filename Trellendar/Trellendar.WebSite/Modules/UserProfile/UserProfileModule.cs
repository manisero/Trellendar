using Nancy;
using Nancy.Security;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        public UserProfileModule() : base("UserProfile")
        {
            this.RequiresAuthentication();

            Get["/"] = Index;
        }

        public dynamic Index(dynamic parameters)
        {
            return View["index"];
        }
    }
}