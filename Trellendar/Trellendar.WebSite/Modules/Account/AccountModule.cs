using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;

namespace Trellendar.WebSite.Modules.Account
{
    public class AccountModule : NancyModule
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi) : base("account")
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;

            Get["/Create"] = _ => new RedirectResponse(_calendarAuthorizationApi.GetAuthorizationUri());
        }
    }
}