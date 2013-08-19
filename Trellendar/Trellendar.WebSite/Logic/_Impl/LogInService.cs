using System;
using Nancy;
using Nancy.Session;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Logic.UserManagement;

namespace Trellendar.WebSite.Logic._Impl
{
    public class LogInService : ILogInService
    {
        private const string AUTHORIZATION_STATE_PARAMETER_NAME = "authorization_state";

        private readonly ICalendarAuthorizationAPI _calendarAuthorizationApi;
        private readonly ITrelloAuthorizationAPI _trelloAuthorizationApi;
        private readonly IUserService _userService;

        public LogInService(ICalendarAuthorizationAPI calendarAuthorizationApi, ITrelloAuthorizationAPI trelloAuthorizationApi, IUserService userService)
        {
            _calendarAuthorizationApi = calendarAuthorizationApi;
            _trelloAuthorizationApi = trelloAuthorizationApi;
            _userService = userService;
        }

        public string PrepareGoogleAuthorizationUri(ISession session, string redirectUri)
        {
            var authorizationState = Guid.NewGuid().ToString();
            session[AUTHORIZATION_STATE_PARAMETER_NAME] = authorizationState;

            return _calendarAuthorizationApi.GetAuthorizationUri(redirectUri, authorizationState);
        }

        public string GetTrelloAuthorizationUri()
        {
            return _trelloAuthorizationApi.GetAuthorizationUri();
        }

        public bool TryLogUserIn(Request request, ISession session, string redirectUri, out Guid userId)
        {
            var state = request.Query["state"];
            var expectedState = session[AUTHORIZATION_STATE_PARAMETER_NAME] as string;

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

        public Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken)
        {
            return _userService.RegisterUser(unregisteredUserId, trelloAccessToken);
        }
    }
}