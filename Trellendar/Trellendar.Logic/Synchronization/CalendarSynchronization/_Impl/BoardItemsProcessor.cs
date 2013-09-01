using System.Collections.Generic;
using System.Linq;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization._Impl
{
    public class BoardItemsProcessor : IBoardItemsProcessor
    {
        private readonly BoardCalendarContext _boardCalendarContext;
        private readonly ISingleBoardItemProcessorFactory _singleBoardItemProcessorFactory;
        private readonly ICalendarAPI _calendarApi;

        protected BoardCalendarBond BoardCalendarBond
        {
            get { return _boardCalendarContext.BoardCalendarBond; }
        }

        public BoardItemsProcessor(BoardCalendarContext boardCalendarContext, ISingleBoardItemProcessorFactory singleBoardItemProcessorFactory, 
                                   ICalendarAPI calendarApi)
        {
            _boardCalendarContext = boardCalendarContext;
            _singleBoardItemProcessorFactory = singleBoardItemProcessorFactory;
            _calendarApi = calendarApi;
        }

        public void Process<TItem>(IEnumerable<TItem> items, IEnumerable<Event> events)
        {
            var itemProcessor = _singleBoardItemProcessorFactory.Create<TItem>();

            foreach (var item in items)
            {
                var itemId = itemProcessor.GetItemID(item);
                var existingEvent = events.SingleOrDefault(x => x.GetSourceID() == itemId);

                var newEvent = itemProcessor.Process(item);

                if (newEvent == null)
                {
                    if (existingEvent != null)
                    {
                        _calendarApi.DeleteEvent(BoardCalendarBond.CalendarID, existingEvent);
                    }

                    continue;
                }

                if (existingEvent == null)
                {
                    _calendarApi.CreateEvent(BoardCalendarBond.CalendarID, newEvent);
                }
                else
                {
                    newEvent.Id = existingEvent.Id;
                    newEvent.Sequence = existingEvent.Sequence + 1;
                    _calendarApi.UpdateEvent(BoardCalendarBond.CalendarID, newEvent);
                }
            }
        }
    }
}
