using System;
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
            Get["/OAuthCallback"] = OAuthCallback;
        }

        public dynamic Create(dynamic parameters)
        {
            return new RedirectResponse(_calendarAuthorizationApi.GetAuthorizationUri("http://localhost:12116/Account/OAuthCallback"));
        }

        public dynamic OAuthCallback(dynamic parameters)
        {
            var authorizationCode = Context.Request.Query["code"];

            if (!authorizationCode.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'code' parameter");
            }

            return authorizationCode.Value;
        }
    }
}