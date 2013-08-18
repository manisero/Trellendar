using System;
using Nancy;
using Nancy.Responses;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.UserManagement;
using Nancy.Authentication.Forms;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class AccountModule : NancyModule
    {
        private const string LOG_IN_CALLBACK_ACTION = "/LogInCallback";
        private const string AUTHORIZATION_STATE_PARAMETER_NAME = "state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;
        private readonly IUserService _userService;

        public AccountModule(ICalendarAuthorizationAPI calendarAuthorizationApi, IUserService userService)
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;
            _userService = userService;

            Get["/LogIn"] = LogIn;
            Get[LOG_IN_CALLBACK_ACTION] = LogInCallback;
        }

        public dynamic LogIn(dynamic parameters)
        {
            var authorizationState = Guid.NewGuid().ToString();

            Session[AUTHORIZATION_STATE_PARAMETER_NAME] = authorizationState;
            var authorizationUri = _calendarAuthorizationApi.GetAuthorizationUri(GetAuthorizationRedirectUri(), authorizationState);

            return new RedirectResponse(authorizationUri);
        }

        public dynamic LogInCallback(dynamic parameters)
        {
            var state = Context.Request.Query["state"];
            var expectedState = Session[AUTHORIZATION_STATE_PARAMETER_NAME] as string;

            Session.Delete(AUTHORIZATION_STATE_PARAMETER_NAME);

            if (!state.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'state' parameter");
            }

            if (expectedState == null || state.Value != expectedState)
            {
                throw new InvalidOperationException("The state specified does not match the expected state");
            }

            var authorizationCode = Context.Request.Query["code"];

            if (!authorizationCode.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'code' parameter");
            }

            User user = _userService.GetOrCreateUser(authorizationCode.Value, GetAuthorizationRedirectUri());

            return this.LoginAndRedirect(user.UserID);
        }

        public dynamic LogOut(dynamic parameters)
        {
            return this.Logout("/");
        }

        private string GetAuthorizationRedirectUri()
        {
            return Request.Url.SiteBase + LOG_IN_CALLBACK_ACTION;
        }
    }
}