using System;
using Nancy;
using Nancy.Session;

namespace Trellendar.WebSite.Logic
{
    public interface ILogInService
    {
        string PrepareGoogleAuthorizationUri(ISession session, string redirectUri);

        bool TryLogUserIn(Request request, ISession session, string redirectUri, out Guid userId);

        string GetTrelloAuthorizationUri();

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);
    }
}
