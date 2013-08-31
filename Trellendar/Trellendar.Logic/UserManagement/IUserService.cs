using System;
using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserService
    {
        bool TryGetUserID(string authorizationCode, string authorizationRedirectUri, out Guid userId);

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);

        IList<Calendar> GetAvailableCalendars();
    }
}
