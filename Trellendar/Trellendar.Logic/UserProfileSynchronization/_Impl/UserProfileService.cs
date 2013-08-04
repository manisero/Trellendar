using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using System.Linq;

namespace Trellendar.Logic.UserProfileSynchronization._Impl
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProfileSynchronizaionSettingsProvider _settingsProvider;

        public UserProfileService(UserContext userContext, IUnitOfWork unitOfWork, IUserProfileSynchronizaionSettingsProvider settingsProvider)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _settingsProvider = settingsProvider;
        }

        public void UpdateUser(Board userBoard, Calendar userCalendar)
        {
            _userContext.User.CalendarTimeZone = userCalendar.TimeZone;

            _unitOfWork.SaveChanges();
        }

        public void UpdateUserPreferences(Board userBoard)
        {
            var configurationCard = userBoard.Cards.FirstOrDefault(x => x.Name == _settingsProvider.TrellendarConfigurationTrelloCardName);

            if (configurationCard == null)
            {
                return;
            }


        }
    }
}