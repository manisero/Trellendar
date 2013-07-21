using System.Collections.Generic;
using System.Linq;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization
{
    public abstract class BoardItemsProcessorBase<TItem> : IBoardItemsProcessor<TItem>
    {
        private readonly UserContext _userContext;
        private readonly ICalendarAPI _calendarApi;

        private User User
        {
            get { return _userContext.User; }
        }

        protected BoardItemsProcessorBase(UserContext userContext, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _calendarApi = calendarApi;
        }

        public void Process(IEnumerable<TItem> items, string itemParentName, IEnumerable<Event> events)
        {
            foreach (var item in items)
            {
                var itemId = GetItemID(item);
                var existingEvent = events.SingleOrDefault(x => x.GetExtendedProperty(EventExtensions.SOURCE_ID_PROPERTY_KEY) == itemId);

                var newEvent = ProcessItem(item, itemParentName);

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
                    _calendarApi.UpdateEvent(User.CalendarID, newEvent);
                }
            }
        }

        protected abstract string GetItemID(TItem item);

        protected abstract Event ProcessItem(TItem item, string parentName);
    }
}
