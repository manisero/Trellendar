using System;
using Nancy;
using Nancy.Session;

namespace Trellendar.WebSite.Logic
{
    public interface ILogInService
    {
        string PrepareAuthorizationUri(ISession session, string redirectUri);

        bool TryLogUserIn(Request request, ISession session, string redirectUri, out Guid userId);
    }
}
