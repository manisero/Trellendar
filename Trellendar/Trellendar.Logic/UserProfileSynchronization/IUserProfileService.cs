using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserProfileSynchronization
{
    public interface IUserProfileService
    {
        void UpdateUserProfile(Board userBoard, Calendar userCalendar);
    }
}
