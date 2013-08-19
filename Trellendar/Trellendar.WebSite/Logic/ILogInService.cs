using System;
using Nancy;
using Nancy.Session;

namespace Trellendar.WebSite.Logic
{
    public interface ILogInService
    {
        string PrepareGoogleAuthorizationUri(ISession session, string redirectUri);

        string GetTrelloAuthorizationUri();

        bool TryLogUserIn(Request request, ISession session, string redirectUri, out Guid userId);

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);
    }
}
