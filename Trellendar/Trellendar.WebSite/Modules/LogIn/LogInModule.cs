using System;
using Nancy;
using Nancy.Responses;
using Nancy.Authentication.Forms;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.WebSite.Logic;
using Nancy.ModelBinding;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class LogInModule : NancyModule
    {
        private const string GOOGLE_LOG_IN_CALLBACK_ACTION = "/GoogleLogInCallback";

        private readonly ILogInService _logInService;
        private readonly ITrelloAuthorizationAPI _trelloAuthorizationApi;

        public LogInModule(ILogInService logInService, ITrelloAuthorizationAPI trelloAuthorizationApi)
        {
            _logInService = logInService;
            _trelloAuthorizationApi = trelloAuthorizationApi;

            Get["/LogIn"] = LogIn;
            Get[GOOGLE_LOG_IN_CALLBACK_ACTION] = GoogleLogInCallback;
            Get["/TrelloLogInCallback"] = TrelloLogInCallback;
            Post["/TrelloLogInCallback"] = TrelloLogInCallback;
            Get["/LogOut"] = LogOut;
        }

        public dynamic LogIn(dynamic parameters)
        {
            var authorizationUri = _logInService.PrepareAuthorizationUri(Session, FormatAuthorizationRedirectUri());

            return new RedirectResponse(authorizationUri);
        }

        public dynamic GoogleLogInCallback(dynamic parameters)
        {
            Guid userId;

            if (_logInService.TryLogUserIn(Context.Request, Session, FormatAuthorizationRedirectUri(), out userId))
            {
                return this.LoginAndRedirect(userId);
            }
            else
            {
                var model = new TrelloLogInModel
                {
                    AuthorizationUrl = _trelloAuthorizationApi.GetAuthorizationUri(),
                    UserID = userId.ToString()
                };

                return View[model];
            }
        }

        public dynamic TrelloLogInCallback(dynamic parameters)
        {
            var model = this.Bind<TrelloLogInResultModel>();
            var userId = _logInService.RegisterUser(model.UserID, model.AccessToken);

            return this.LoginAndRedirect(userId);
        }

        public dynamic LogOut(dynamic parameters)
        {
            return this.Logout("/");
        }

        private string FormatAuthorizationRedirectUri()
        {
            return Request.Url.SiteBase + GOOGLE_LOG_IN_CALLBACK_ACTION;
        }
    }
}