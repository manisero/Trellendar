using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private const int DEFAULT_EVENT_LENGTH = 1;

        private readonly UserContext _userContext;

        public CardProcessor(UserContext userContext)
        {
            _userContext = userContext;
        }

        public string GetItemID(Card item)
        {
            return item.Id;
        }

        public Event Process(Card item, string parentName)
        {
            if (item.Closed || item.Due == null)
            {
                return null;
            }

            return new Event
            {
                summary = FormatEventSummary(item.Name, parentName),
                start = new TimeStamp(item.Due.Value),
                end = new TimeStamp(item.Due.Value.AddHours(DEFAULT_EVENT_LENGTH)),
                extendedProperties = new EventExtendedProperties
                {
                    @private = new Dictionary<string, string> { { EventExtensions.SOURCE_ID_PROPERTY_KEY, item.Id } }
                }
            };
        }

        private string FormatEventSummary(string cardName, string listName)
        {
            var eventNameTemplate = _userContext.GetPrefferedCardEventNameTemplate();

            return eventNameTemplate != null
                       ? eventNameTemplate.FormatWith(listName, cardName) // TODO: extract parent name shortcut
                       : cardName;
        }
    }
}
