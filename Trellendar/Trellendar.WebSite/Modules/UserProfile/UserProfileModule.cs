using System.Linq;
using Nancy;
using Nancy.Security;
using Trellendar.Logic;
using Trellendar.WebSite.Modules.UserProfile.Models;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        private readonly UserContext _userContext;

        public UserProfileModule(UserContext userContext)
            : base("UserProfile")
        {
            _userContext = userContext;

            this.RequiresAuthentication();

            Get["/"] = Index;
        }

        public dynamic Index(dynamic parameters)
        {
            var model = new IndexModel
                {
                    Email = _userContext.User.Email,
                    BoardCalendarBonds = _userContext.User.BoardCalendarBonds.Select(x => new BoardCalendarBondModel
                        {
                            BoardName = x.BoardID,
                            CalendarName = x.CalendarID
                        })
                };

            return View[model];
        }
    }
}