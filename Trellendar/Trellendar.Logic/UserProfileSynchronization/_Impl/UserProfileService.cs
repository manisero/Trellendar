using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using System.Linq;

namespace Trellendar.Logic.UserProfileSynchronization._Impl
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IUserProfileSynchronizaionSettingsProvider _settingsProvider;

        public UserProfileService(UserContext userContext, IUnitOfWork unitOfWork, IJsonSerializer jsonSerializer, IUserProfileSynchronizaionSettingsProvider settingsProvider)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _jsonSerializer = jsonSerializer;
            _settingsProvider = settingsProvider;
        }

        public void UpdateUser(Board userBoard, Calendar userCalendar)
        {
            _userContext.User.CalendarTimeZone = userCalendar.TimeZone;

            _unitOfWork.SaveChanges();
        }

        public void UpdateUserPreferences(Board userBoard)
        {
            if (userBoard.Cards == null)
            {
                return;
            }

            var configurationCard = userBoard.Cards.FirstOrDefault(x => x.Name == _settingsProvider.TrellendarConfigurationTrelloCardName);

            if (configurationCard == null)
            {
                return;
            }

            var preferences = _jsonSerializer.Deserialize<UserPreferences>(configurationCard.Desc);

            if (preferences == null)
            {
                return;
            }

            _userContext.User.UserPreferences = preferences;
            _unitOfWork.SaveChanges();
        }
    }
}