using Nancy;
using Nancy.Security;
using Trellendar.Logic;
using Trellendar.WebSite.Modules.BondSettings.Models;

namespace Trellendar.WebSite.Modules.BondSettings
{
    public class BondSettingsModule : NancyModule
    {
        private readonly UserContext _userContext;

        public BondSettingsModule(UserContext userContext)
            : base("BondSettings")
        {
            _userContext = userContext;
            this.RequiresAuthentication();

            Get["/"] = Index;
            Post["/Save"] = Save;
        }

        public dynamic Index(dynamic parameters)
        {
            var model = new IndexModel();



            return View[model];
        }

        public dynamic Save(dynamic parameters)
        {
            return null;
        }
    }
}