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
        private readonly IBoardItemsProcessor _boardItemsProcessor;

        private User User
        {
            get { return _userContext.User; }
        }

        public CalendarService(UserContext userContext, IBoardItemsProcessor boardItemsProcessor)
        {
            _userContext = userContext;
            _boardItemsProcessor = boardItemsProcessor;
        }

        public void UpdateCalendar(Board board, IEnumerable<Event> events)
        {
            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id && x.DateLastActivity > User.LastSynchronizationTS);
                _boardItemsProcessor.Process(cards, list.Name, events);
            }
        }
    }
}