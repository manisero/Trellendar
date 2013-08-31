using Nancy;
using Nancy.Security;
using Trellendar.Logic;
using Trellendar.Logic.UserManagement;
using Trellendar.WebSite.Modules.UserProfile.Models;
using System.Linq;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        private readonly UserContext _userContext;
        private readonly IUserService _userService;

        public UserProfileModule(UserContext userContext, IUserService userService)
            : base("UserProfile")
        {
            _userContext = userContext;
            _userService = userService;

            this.RequiresAuthentication();

            Get["/"] = Index;
        }

        public dynamic Index(dynamic parameters)
        {
            var boards = _userService.GetAvailableBoards();
            var calendars = _userService.GetAvailableCalendars();

            var model = new IndexModel
                {
                    Email = _userContext.User.Email,
                    AvailableBoards = boards.ToDictionary(x => x.Id, x => x.Name),
                    AvailableCalendars = calendars.ToDictionary(x => x.Id, x => x.Summary)
                };

            return View[model];
        }
    }
}