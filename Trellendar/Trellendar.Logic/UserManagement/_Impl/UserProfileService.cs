using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using System.Linq;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserSynchronizationService : IUserSynchronizationService
    {
        private readonly UserContext _userContext;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSynchronizationSettingsProvider _settingsProvider;

        public UserSynchronizationService(UserContext userContext, IJsonSerializer jsonSerializer, IRepositoryFactory repositoryFactory,
                                          IUnitOfWork unitOfWork, IUserSynchronizationSettingsProvider settingsProvider)
        {
            _userContext = userContext;
            _jsonSerializer = jsonSerializer;
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
            _settingsProvider = settingsProvider;
        }

        public void UpdateUser(Calendar userCalendar)
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

            var preferences = _jsonSerializer.Deserialize<UserPreferences>(configurationCard.Description);

            if (preferences == null)
            {
                return;
            }

			if (_userContext.HasUserPreferences())
			{
				_repositoryFactory.Create<UserPreferences>().Remove(_userContext.User.UserPreferences);
			}
			
            _userContext.User.UserPreferences = preferences;
            _unitOfWork.SaveChanges();
        }
    }
}