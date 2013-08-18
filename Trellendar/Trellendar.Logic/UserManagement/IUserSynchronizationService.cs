using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserManagement
{
    public interface IUserSynchronizationService
    {
        void UpdateUser(Calendar userCalendar);

        void UpdateUserPreferences(Board userBoard);
    }
}
