using System;
using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserService
    {
        bool TryGetUserID(string userEmail, out Guid userId);

        bool TryCreateUnregisteredUser(Token token, out Guid unregisteredUserId);

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);

        IEnumerable<Board> GetAvailableBoards();

        IEnumerable<Calendar> GetAvailableCalendars();
    }
}
