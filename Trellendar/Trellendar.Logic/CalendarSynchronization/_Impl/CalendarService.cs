using System.Collections.Generic;
using System.Linq;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class CalendarService : ICalendarService
    {
        private readonly UserContext _userContext;
        private readonly ICardProcessor _cardProcessor;
        private readonly ICalendarAPI _calendarApi;

        private User User
        {
            get { return _userContext.User; }
        }

        public CalendarService(UserContext userContext, ICardProcessor cardProcessor, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _cardProcessor = cardProcessor;
            _calendarApi = calendarApi;
        }

        public void UpdateCalendar(Board board, IEnumerable<Event> events)
        {
            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id && x.DateLastActivity > User.LastSynchronizationTS);

                foreach (var card in cards)
                {
                    var existingEvent = events.SingleOrDefault(x => x.GetExtendedProperty(EventExtensions.SOURCE_ID_PROPERTY_KEY) == card.Id);
                    var newEvent = _cardProcessor.Process(card, list.Name);

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
        }
    }
}