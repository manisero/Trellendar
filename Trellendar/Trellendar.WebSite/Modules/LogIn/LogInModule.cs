using Nancy;
using Nancy.Responses;
using Nancy.Authentication.Forms;
using Trellendar.WebSite.Logic;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class AccountModule : NancyModule
    {
        private const string LOG_IN_CALLBACK_ACTION = "/LogInCallback";

        private readonly ILogInService _logInService;

        public AccountModule(ILogInService logInService)
        {
            _logInService = logInService;

            Get["/LogIn"] = LogIn;
            Get[LOG_IN_CALLBACK_ACTION] = LogInCallback;
        }

        public dynamic LogIn(dynamic parameters)
        {
            var authorizationUri = _logInService.PrepareAuthorizationUri(Session, FormatAuthorizationRedirectUri());

            return new RedirectResponse(authorizationUri);
        }

        public dynamic LogInCallback(dynamic parameters)
        {
            var user = _logInService.HandleLoginCallback(Context.Request, Session, FormatAuthorizationRedirectUri());

            return this.LoginAndRedirect(user.UserID);
        }

        public dynamic LogOut(dynamic parameters)
        {
            return this.Logout("/");
        }

        private string FormatAuthorizationRedirectUri()
        {
            return Request.Url.SiteBase + LOG_IN_CALLBACK_ACTION;
        }
    }
}