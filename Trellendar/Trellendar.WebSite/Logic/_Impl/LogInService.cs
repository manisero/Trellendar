using System;
using Nancy;
using Nancy.Session;
using Trellendar.DataAccess.Remote.Google;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Google;
using Trellendar.Logic.UserManagement;

namespace Trellendar.WebSite.Logic._Impl
{
    public class LogInService : ILogInService
    {
        private const string AUTHORIZATION_STATE_PARAMETER_NAME = "authorization_state";

        private readonly IGoogleAuthorizationAPI _googleAuthorizationApi;
        private readonly ITrelloAuthorizationAPI _trelloAuthorizationApi;
        private readonly IUserRegistrationService _userRegistrationService;

        public LogInService(IGoogleAuthorizationAPI googleAuthorizationApi, ITrelloAuthorizationAPI trelloAuthorizationApi,
                            IUserRegistrationService userRegistrationService)
        {
            _googleAuthorizationApi = googleAuthorizationApi;
            _trelloAuthorizationApi = trelloAuthorizationApi;
            _userRegistrationService = userRegistrationService;
        }

        public string PrepareGoogleAuthorizationUri(ISession session, string redirectUri, bool forNewUser = false)
        {
            var authorizationState = Guid.NewGuid().ToString();
            session[AUTHORIZATION_STATE_PARAMETER_NAME] = authorizationState;

            return _googleAuthorizationApi.GetAuthorizationUri(redirectUri, authorizationState, forNewUser);
        }

        public Token GetToken(Request request, ISession session, string redirectUri)
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

            return _googleAuthorizationApi.GetToken(authorizationCode.Value, redirectUri);
        }

        public bool TryGetUserID(Token token, out Guid userId)
        {
            return _userRegistrationService.TryGetUserID(token.UserEmail, out userId);
        }

        public bool TryCreateUnregisteredUser(Token token, out Guid unregisteredUserId)
        {
            return _userRegistrationService.TryCreateUnregisteredUser(token, out unregisteredUserId);
        }

        public string GetTrelloAuthorizationUri()
        {
            return _trelloAuthorizationApi.GetAuthorizationUri();
        }

        public Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken)
        {
            return _userRegistrationService.RegisterUser(unregisteredUserId, trelloAccessToken);
        }
    }
}