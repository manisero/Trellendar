using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserService
    {
        User GetOrCreateUser(string authorizationCode, string authorizationRedirectUri);

        User GetUser(string userEmail);

        IList<Calendar> GetAvailableCalendars();
    }
}
