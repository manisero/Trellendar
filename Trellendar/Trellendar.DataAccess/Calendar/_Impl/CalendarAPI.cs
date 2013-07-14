using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.Domain.Calendar;
using Trellendar.Core.Extensions;

namespace Trellendar.DataAccess.Calendar._Impl
{
    public class CalendarAPI : ICalendarAPI
    {
        private readonly ICalendarClient _calendarClient;
        private readonly IJsonSerializer _jsonSerializer;

        public CalendarAPI(IAuthorizedCalendarClient calendarClient, IJsonSerializer jsonSerializer)
        {
            _calendarClient = calendarClient;
            _jsonSerializer = jsonSerializer;
        }

        public Domain.Calendar.Calendar GetCalendar(string calendarId)
        {
            var calendar = _calendarClient.Get("calendars/{0}".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<Domain.Calendar.Calendar>(calendar);
        }

        public IEnumerable<Event> GetEvents(string calendarId)
        {
            var events = _calendarClient.Get("calendars/{0}/events".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<CalendarEvents>(events).Items;
        }

        public void CreateEvent(string calendarId, Event @event)
        {
            _calendarClient.Post("calendars/{0}/events".FormatWith(calendarId), _jsonSerializer.Serialize(@event));
        }
    }
}