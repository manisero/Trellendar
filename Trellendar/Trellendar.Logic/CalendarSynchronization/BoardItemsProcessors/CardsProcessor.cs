using System.Collections.Generic;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.BoardItemsProcessors
{
    public class CardsProcessor : BoardItemsProcessorBase<Card>
    {
        public CardsProcessor(UserContext userContext, ICalendarAPI calendarApi)
            : base(userContext, calendarApi)
        {
        }

        protected override string GetItemID(Card item)
        {
            return item.Id;
        }

        protected override Event ProcessItem(Card item, string parentName)
        {
            if (item.Closed || item.Due == null)
            {
                return null;
            }

            var @event = new Event
            {
                summary = item.Name,
                start = new TimeStamp(item.Due.Value),
                end = new TimeStamp(item.Due.Value.AddHours(1)),
                extendedProperties = new EventExtendedProperties
                {
                    @private = new Dictionary<string, string> { { EventExtensions.SOURCE_ID_PROPERTY_KEY, item.Id } }
                }
            };

            return @event;
        }
    }
}
