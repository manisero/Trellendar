﻿using System;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.CalendarSynchronization.Formatters;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CardProcessor : ISingleBoardItemProcessor<Card>
    {
        private readonly UserContext _userContext;
        private readonly ICardEventSummaryFormatter _eventSummaryFormatter;
        private readonly IParser<Due> _dueParser;
        private readonly IParser<Location> _locationParser;
        private readonly IEventTimeFrameCreator _eventTimeFrameCreator;
        private readonly ICardEventDescriptionFormatter _eventDescriptionFormatter;

        public CardProcessor(UserContext userContext, ICardEventSummaryFormatter eventSummaryFormatter, IParser<Due> dueParser, IParser<Location> locationParser,
                             IEventTimeFrameCreator eventTimeFrameCreator, ICardEventDescriptionFormatter eventDescriptionFormatter)
        {
            _userContext = userContext;
            _eventSummaryFormatter = eventSummaryFormatter;
            _dueParser = dueParser;
            _locationParser = locationParser;
            _eventTimeFrameCreator = eventTimeFrameCreator;
            _eventDescriptionFormatter = eventDescriptionFormatter;
        }

        public string GetItemID(Card item)
        {
            return item.Id;
        }

        public Event Process(Card item, string parentName)
        {
            if (item.Closed)
            {
                return null;
            }

            Tuple<TimeStamp, TimeStamp> timeFrame;

            if (item.Due != null)
            {
                timeFrame = _eventTimeFrameCreator.CreateFromUTC(item.Due.Value, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime());
            }
            else
            {
                var due = _dueParser.Parse(item.Description, _userContext.GetUserPreferences());

                if (due == null)
                {
                    return null;
                }

                timeFrame = due.HasTime
                                ? _eventTimeFrameCreator.CreateFromLocal(due.DueDateTime, _userContext.GetCalendarTimeZone(), _userContext.GetPrefferedWholeDayEventDueTime())
                                : _eventTimeFrameCreator.CreateWholeDayTimeFrame(due.DueDateTime);
            }

            var summary = _eventSummaryFormatter.Format(item, _userContext.GetUserPreferences());
            var location = _locationParser.Parse(item.Description, _userContext.GetUserPreferences());
            var description = _eventDescriptionFormatter.Format(item);

            return new Event
                {
                    Summary = summary,
                    Start = timeFrame.Item1,
                    End = timeFrame.Item2,
                    Location = location != null ? location.Value : null,
                    Description = description,
                    ExtendedProperties = EventExtensions.CreateExtendedProperties(item.Id)
                };
        }
    }
}
