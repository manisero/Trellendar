using System.Collections.Generic;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
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

            var @event = new Event
            {
                summary = _userContext.User.UserPreferences.CardEventNameTemplate.FormatWith(parentName, item.Name), // TODO: extract parent name shortcut
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
