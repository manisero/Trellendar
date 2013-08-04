using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserProfileSynchronization
{
    public interface IUserProfileService
    {
        void UpdateUser(Board userBoard, Calendar userCalendar);

        void UpdateUserPreferences(Board userBoard);
    }
}
