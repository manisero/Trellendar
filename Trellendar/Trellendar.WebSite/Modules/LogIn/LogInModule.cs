using System;
using Nancy;
using Nancy.Responses;
using Nancy.Authentication.Forms;
using Trellendar.WebSite.Logic;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class LogInModule : NancyModule
    {
        private const string GOOGLE_LOG_IN_CALLBACK_ACTION = "/GoogleLogInCallback";

        private readonly ILogInService _logInService;

        public LogInModule(ILogInService logInService)
        {
            _logInService = logInService;

            Get["/LogIn"] = LogIn;
            Get[GOOGLE_LOG_IN_CALLBACK_ACTION] = GoogleLogInCallback;
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
                return View["TrelloLogIn", userId.ToString()];
            }
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