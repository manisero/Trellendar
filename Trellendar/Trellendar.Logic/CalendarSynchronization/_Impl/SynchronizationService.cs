using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;

        public SynchronizationService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(_userContext.User.TrelloBoardID);
        }
    }
}