using System.Collections.Generic;
using System.Linq;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization._Impl
{
    public class CalendarSynchronizationService : ICalendarSynchronizationService
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly IBoardItemsProcessor _boardItemsProcessor;

        public CalendarSynchronizationService(BoardCalendarContext boardCalendarContext, IBoardItemsProcessor boardItemsProcessor)
        {
            _boardCalendarContext = boardCalendarContext;
            _boardItemsProcessor = boardItemsProcessor;
        }

        public void UpdateCalendar(Board board, IEnumerable<Event> events)
        {
            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id &&
                                                   x.DateLastActivity > _boardCalendarContext.BoardCalendarBond.LastSynchronizationTS);

                if (cards.Any())
                {
                    _boardItemsProcessor.Process(cards, events);
                }

                var checklists = board.CheckLists.Where(x => cards.Any(card => card.Id == x.IdCard));

                foreach (var checklist in checklists)
                {
                    _boardItemsProcessor.Process(checklist.CheckItems, events);
                }
            }
        }
    }
}