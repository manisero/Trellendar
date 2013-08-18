using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;

namespace Trellendar.WebSite.Modules.Account
{
    public class AccountModule : NancyModule
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi) : base("Account")
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;

            Get["/Create"] = Create;
        }

        public dynamic Create(dynamic parameters)
        {
            return new RedirectResponse(_calendarAuthorizationApi.GetAuthorizationUri("urn:ietf:wg:oauth:2.0:oob"));
        }
    }
}