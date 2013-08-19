using Nancy;
using Nancy.Security;
using Trellendar.Logic.UserManagement;
using Trellendar.WebSite.Logic;
using Trellendar.WebSite.Modules.UserProfile.Models;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        private readonly IUserContextRegistrar _userContextRegistrar;
        private readonly IUserService _userService;

        public UserProfileModule(IUserContextRegistrar userContextRegistrar, IUserService userService)
            : base("UserProfile")
        {
            _userContextRegistrar = userContextRegistrar;
            _userService = userService;

            this.RequiresAuthentication();
            Before += RegisterUserContext;

            Get["/"] = Index;
        }

        private Response RegisterUserContext(NancyContext context)
        {
            _userContextRegistrar.Register(context.CurrentUser.UserName);
            return null;
        }

        public dynamic Index(dynamic parameters)
        {
            var user = _userService.GetUser(Context.CurrentUser.UserName);
            var calendars = _userService.GetAvailableCalendars();

            var model = new IndexModel { Email = user.Email };

            return View[model];
        }
    }
}