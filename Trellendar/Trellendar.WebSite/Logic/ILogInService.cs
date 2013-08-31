using System;
using Nancy;
using Nancy.Session;
using Trellendar.Domain.Google;

namespace Trellendar.WebSite.Logic
{
    public interface ILogInService
    {
        string PrepareGoogleAuthorizationUri(ISession session, string redirectUri, bool forNewUser = false);

        Token GetToken(Request request, ISession session, string redirectUri);

        bool TryGetUserID(Token token, out Guid userId);

        bool TryCreateUnregisteredUser(Token token, out Guid unregisteredUserId);

        string GetTrelloAuthorizationUri();

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);
    }
}
