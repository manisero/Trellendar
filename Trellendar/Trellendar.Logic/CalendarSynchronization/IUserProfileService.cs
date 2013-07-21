using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IUserProfileService
    {
        void UpdateUserProfile(Board userBoard, Calendar userCalendar);
    }
}
