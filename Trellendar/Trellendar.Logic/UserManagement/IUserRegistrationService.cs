using System;
using Trellendar.Domain.Google;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserRegistrationService
    {
        bool TryGetUserID(string userEmail, out Guid userId);

        bool TryCreateUnregisteredUser(Token token, out Guid unregisteredUserId);

        Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken);
    }
}
