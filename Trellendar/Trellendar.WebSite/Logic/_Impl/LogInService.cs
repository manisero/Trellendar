using System;
using Nancy;
using Nancy.Session;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Logic.UserManagement;

namespace Trellendar.WebSite.Logic._Impl
{
    public class LogInService : ILogInService
    {
        private const string AUTHORIZATION_STATE_PARAMETER_NAME = "authorization_state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;
        private readonly IUserService _userService;

        public LogInService(ICalendarAuthorizationAPI calendarAuthorizationApi, IUserService userService)
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;
            _userService = userService;
        }

        public string PrepareAuthorizationUri(ISession session, string redirectUri)
        {
            var authorizationState = Guid.NewGuid().ToString();
            session[AUTHORIZATION_STATE_PARAMETER_NAME] = authorizationState;

            return _calendarAuthorizationApi.GetAuthorizationUri(redirectUri, authorizationState);
        }

        public bool TryLogUserIn(Request request, ISession session, string redirectUri, out Guid userId)
        {
            var state = request.Query["state"];
            var expectedState = session[AUTHORIZATION_STATE_PARAMETER_NAME] as string;

            session.Delete(AUTHORIZATION_STATE_PARAMETER_NAME);

            if (!state.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'state' parameter");
            }

            if (expectedState == null || state.Value != expectedState)
            {
                throw new InvalidOperationException("The state specified does not match the expected state");
            }

            var authorizationCode = request.Query["code"];

            if (!authorizationCode.HasValue)
            {
                throw new InvalidOperationException("The request query should contain 'code' parameter");
            }

            return _userService.TryGetUserID(authorizationCode.Value, redirectUri, out userId);
        }
    }
}