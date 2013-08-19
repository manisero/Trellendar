using System;
using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserService
    {
        bool TryGetUserID(string authorizationCode, string authorizationRedirectUri, out Guid userId);

        User GetUser(string userEmail);

        IList<Calendar> GetAvailableCalendars();
    }
}
