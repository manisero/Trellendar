using Nancy;
using Nancy.Security;
using Trellendar.Logic.UserManagement;
using Trellendar.WebSite.Modules.UserProfile.Models;
using System.Linq;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        private readonly IUserService _userService;

        public UserProfileModule(IUserService userService)
            : base("UserProfile")
        {
            _userService = userService;

            this.RequiresAuthentication();

            Get["/"] = Index;
        }

        public dynamic Index(dynamic parameters)
        {
            var user = _userService.GetUser(Context.CurrentUser.UserName);
            var calendars = _userService.GetAvailableCalendars();

            var model = new IndexModel
                {
                    Email = user.Email,
                    AvailableCalendars = calendars.ToDictionary(x => x.Id, x => x.Summary)
                };

            return View[model];
        }
    }
}