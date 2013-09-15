using System.Linq;
using Nancy;
using Nancy.Security;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Logic;
using Trellendar.WebSite.Modules.UserProfile.Models;

namespace Trellendar.WebSite.Modules.UserProfile
{
    public class UserProfileModule : NancyModule
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;

        public UserProfileModule(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi)
            : base("UserProfile")
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;

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
                            BoardName = _trelloApi.GetBoard(x.BoardID).Name,
                            CalendarName = _calendarApi.GetCalendar(x.CalendarID).Summary
                        })
                };

            return View[model];
        }
    }
}