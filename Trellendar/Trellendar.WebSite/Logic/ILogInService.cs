using Nancy;
using Nancy.Session;
using Trellendar.Domain.Trellendar;

namespace Trellendar.WebSite.Logic
{
    public interface ILogInService
    {
        string PrepareAuthorizationUri(ISession session, string redirectUri);

        User HandleLoginCallback(Request request, ISession session, string redirectUri);
    }
}
