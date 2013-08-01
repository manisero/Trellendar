using System;
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
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;

        public CardProcessor(UserContext userContext, IEventTimeFrameCreator eventTimeFrameCreator)
        {
            _userContext = userContext;
            _eventTimeFrameCreator = eventTimeFrameCreator;
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

            var timeFrame = _eventTimeFrameCreator.Create(item.Due.Value, null, new DateTime());

            return new Event
                {
                    summary = FormatEventSummary(item.Name, parentName),
                    start = timeFrame.Item1,
                    end = timeFrame.Item2,
                    extendedProperties = new EventExtendedProperties
                        {
                            @private = new Dictionary<string, string>
                                {
                                    { EventExtensions.GENERATED_PROPERTY_KEY, string.Empty },
                                    { EventExtensions.SOURCE_ID_PROPERTY_KEY, item.Id }
                                }
                        }
                };
        }

        private string FormatEventSummary(string cardName, string listName)
        {
            var eventNameTemplate = _userContext.GetPrefferedCardEventNameTemplate();

            return eventNameTemplate != null
                       ? eventNameTemplate.FormatWith(FormatListName(listName), cardName)
                       : cardName;
        }

        private string FormatListName(string listName)
        {
            var listShortcutMarkers = _userContext.GetPrefferedListShortcutMarkers();

            if (listShortcutMarkers != null &&
                listShortcutMarkers.Item1 != null && listShortcutMarkers.Item2 != null && 
                listName.Contains(listShortcutMarkers.Item1) && listName.Contains(listShortcutMarkers.Item2))
            {
                var beginningIndex = listName.LastIndexOf(listShortcutMarkers.Item1);
                var endIndex = listName.LastIndexOf(listShortcutMarkers.Item2);

                if (endIndex > beginningIndex)
                {
                    return listName.Substring(beginningIndex + listShortcutMarkers.Item1.Length,
                                              endIndex - beginningIndex - listShortcutMarkers.Item2.Length);
                }
            }

            return listName;
        }
    }
}
