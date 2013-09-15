using System;
using Nancy;
using Nancy.Responses;
using Nancy.Authentication.Forms;
using Trellendar.WebSite.Logic;
using Nancy.ModelBinding;
using System.Linq;
using Trellendar.WebSite.Modules.LogIn.Models;

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
            Post["/TrelloLogInCallback"] = TrelloLogInCallback;
            Get["/LogOut"] = LogOut;
        }

        public dynamic LogIn(dynamic parameters)
        {
            var authorizationUri = _logInService.PrepareGoogleAuthorizationUri(Session, FormatAuthorizationRedirectUri());

            return new RedirectResponse(authorizationUri);
        }

        public dynamic GoogleLogInCallback(dynamic parameters)
        {
            Guid userId;

            var token = _logInService.GetToken(Context.Request, Session, FormatAuthorizationRedirectUri());

            if (_logInService.TryGetUserID(token, out userId))
            {
                return this.LoginAndRedirect(userId);
            }
            else if (_logInService.TryCreateUnregisteredUser(token, out userId))
            {
                var model = new TrelloLogInModel
                    {
                        AuthorizationUrl = _logInService.GetTrelloAuthorizationUri(),
                        UserID = userId.ToString()
                    };

                return View[model];
            }
            else
            {
                var authorizationUri = _logInService.PrepareGoogleAuthorizationUri(Session, FormatAuthorizationRedirectUri(), true);

                return new RedirectResponse(authorizationUri);   
            }
        }

        public dynamic TrelloLogInCallback(dynamic parameters)
        {
            var model = this.BindAndValidate<TrelloLogInResultModel>();

            if (!ModelValidationResult.IsValid)
            {
                throw new InvalidOperationException(ModelValidationResult.Errors.Select(x => x.GetMessage(x.MemberNames.First())).First());
            }
            
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