using System;
using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;

namespace Trellendar.WebSite.Modules.Account
{
    public class AccountModule : NancyModule
    {
        private const string LOG_IN_CALLBACK_ACTION = "/Account/LogInCallback";
        private const string AUTHORIZATION_STATE = "state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi) : base("Account")
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;

            Get["/LogIn"] = LogIn;
            Get["/LogInCallback"] = LogInCallback;
        }

        public dynamic LogIn(dynamic parameters)
        {
            var authorizationUri = _calendarAuthorizationApi.GetAuthorizationUri(GetAuthorizationRedirectUri(), AUTHORIZATION_STATE);

            return new RedirectResponse(authorizationUri);
        }

        public dynamic LogInCallback(dynamic parameters)
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

            var token = _calendarAuthorizationApi.GetToken(authorizationCode.Value, GetAuthorizationRedirectUri());
            var userInfo = _calendarAuthorizationApi.GetUserInfo(token.IdToken);

            return "Welcome, " + userInfo.Email;
        }

        private string GetAuthorizationRedirectUri()
        {
            return Request.Url.SiteBase + LOG_IN_CALLBACK_ACTION;
        }
    }
}