using System.Collections.Generic;
using System.Linq;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class CalendarService : ICalendarService
    {
        private readonly UserContext _userContext;
        private readonly IBoardItemsProcessorFactory _boardItemsProcessorFactory;

        private User User
        {
            get { return _userContext.User; }
        }

        public CalendarService(UserContext userContext, IBoardItemsProcessorFactory boardItemsProcessorFactory)
        {
            _userContext = userContext;
            _boardItemsProcessorFactory = boardItemsProcessorFactory;
        }

        public void UpdateCalendar(Board board, IEnumerable<Event> events)
        {
            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id && x.DateLastActivity > User.LastSynchronizationTS);
                _boardItemsProcessorFactory.Create<Card>().Process(cards, list.Name, events);
            }
        }
    }
}