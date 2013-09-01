using Trellendar.Core.Serialization;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using System.Linq;

namespace Trellendar.Logic.Synchronization.BoardCalendarBondSynchronization._Impl
{
    public class BoardCalendarBondSynchronizationService : IBoardCalendarBondSynchronizationService
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBoardCalendarBondSynchronizationSettingsProvider _settingsProvider;

        public BoardCalendarBondSynchronizationService(BoardCalendarContext boardCalendarContext, IJsonSerializer jsonSerializer,
                                                       IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork,
                                                       IBoardCalendarBondSynchronizationSettingsProvider settingsProvider)
        {
            _boardCalendarContext = boardCalendarContext;
            _jsonSerializer = jsonSerializer;
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
            _settingsProvider = settingsProvider;
        }

        public void UpdateBond(Calendar calendar)
        {
            _boardCalendarContext.BoardCalendarBond.CalendarTimeZone = calendar.TimeZone;

            _unitOfWork.SaveChanges();
        }

        public void UpdateBondSettings(Board board)
        {
            if (board.Cards == null)
            {
                return;
            }

            var configurationCard = board.Cards.FirstOrDefault(x => x.Name == _settingsProvider.TrellendarConfigurationTrelloCardName);

            if (configurationCard == null)
            {
                return;
            }

            var preferences = _jsonSerializer.Deserialize<BoardCalendarBondSettings>(configurationCard.Description);

            if (preferences == null)
            {
                return;
            }

			if (_boardCalendarContext.HasBondSettings())
			{
				_repositoryFactory.Create<BoardCalendarBondSettings>().Remove(_boardCalendarContext.BoardCalendarBond.Settings);
			}
			
            _boardCalendarContext.BoardCalendarBond.Settings = preferences;
            _unitOfWork.SaveChanges();
        }
    }
}