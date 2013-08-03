using System.Collections.Generic;
using System.Linq;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class BoardItemsProcessor : IBoardItemsProcessor
    {
        private readonly UserContext _userContext;
        private readonly ISingleBoardItemProcessorFactory _singleBoardItemProcessorFactory;
        private readonly ICalendarAPI _calendarApi;

        protected User User
        {
            get { return _userContext.User; }
        }

        public BoardItemsProcessor(UserContext userContext, ISingleBoardItemProcessorFactory singleBoardItemProcessorFactory, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _singleBoardItemProcessorFactory = singleBoardItemProcessorFactory;
            _calendarApi = calendarApi;
        }

        public void Process<TItem>(IEnumerable<TItem> items, string itemParentName, IEnumerable<Event> events)
        {
            var itemProcessor = _singleBoardItemProcessorFactory.Create<TItem>();

            foreach (var item in items)
            {
                var itemId = itemProcessor.GetItemID(item);
                var existingEvent = events.SingleOrDefault(x => x.GetSourceID() == itemId);

                var newEvent = itemProcessor.Process(item, itemParentName);

                if (newEvent == null)
                {
                    if (existingEvent != null)
                    {
                        _calendarApi.DeleteEvent(User.CalendarID, existingEvent);
                    }

                    continue;
                }

                if (existingEvent == null)
                {
                    _calendarApi.CreateEvent(User.CalendarID, newEvent);
                }
                else
                {
                    newEvent.id = existingEvent.id;
                    newEvent.sequence = existingEvent.sequence + 1;
                    _calendarApi.UpdateEvent(User.CalendarID, newEvent);
                }
            }
        }
    }
}
