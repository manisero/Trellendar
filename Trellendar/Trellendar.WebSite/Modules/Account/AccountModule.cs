using System;
using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;

namespace Trellendar.WebSite.Modules.Account
{
    public class AccountModule : NancyModule
    {
        private const string AUTHORIZATION_STATE = "state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi) : base("Account")
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;

            Get["/Create"] = Create;
            Get["/OAuthCallback"] = OAuthCallback;
        }

        public dynamic Create(dynamic parameters)
        {
            return new RedirectResponse(_calendarAuthorizationApi.GetAuthorizationUri("http://localhost:12116/Account/OAuthCallback", AUTHORIZATION_STATE));
        }

        public dynamic OAuthCallback(dynamic parameters)
        {
            var state = Context.Request.Query["state"];
            
            if (!state.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'state' parameter");
            }

            if (state.Value != AUTHORIZATION_STATE)
            {
                throw new InvalidOperationException("The state specified does not match the expected state");
            }

            var authorizationCode = Context.Request.Query["code"];

            if (!authorizationCode.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'code' parameter");
            }

            return authorizationCode.Value;
        }
    }
}