using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(UserContext userContext, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public void UpdateUserProfile(Board userBoard, Calendar userCalendar)
        {
            _userContext.User.CalendarTimeZone = userCalendar.timeZone;

            _unitOfWork.SaveChanges();
        }
    }
}