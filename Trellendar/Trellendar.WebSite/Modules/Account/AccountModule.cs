using System;
using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Logic.UserManagement;
using Nancy.Authentication.Forms;

namespace Trellendar.WebSite.Modules.Account
{
    public class AccountModule : NancyModule
    {
        private const string LOG_IN_CALLBACK_ACTION = "/Account/LogInCallback";
        private const string AUTHORIZATION_STATE = "state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;
        private readonly IUserService _userService;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi, IUserService userService) : base("Account")
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;
            _userService = userService;

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

            var user = _userService.GetOrCreateUser(authorizationCode.Value, GetAuthorizationRedirectUri());

            return this.LoginAndRedirect(Guid.NewGuid());
        }

        private string GetAuthorizationRedirectUri()
        {
            return Request.Url.SiteBase + LOG_IN_CALLBACK_ACTION;
        }
    }
}