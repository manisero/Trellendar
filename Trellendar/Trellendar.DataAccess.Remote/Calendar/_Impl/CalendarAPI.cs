using System.Collections.Generic;
using Trellendar.Core.Serialization;
using Trellendar.Domain;
using Trellendar.Domain.Calendar;
using Trellendar.Core.Extensions;

namespace Trellendar.DataAccess.Remote.Calendar._Impl
{
    public class CalendarAPI : ICalendarAPI
    {
        private readonly IRestClientFactory _restClientFactory;
        private readonly IJsonSerializer _jsonSerializer;

        private IRestClient _calendarClient;
        private IRestClient CalendarClient
        {
            get { return _calendarClient ?? (_calendarClient = _restClientFactory.CreateAuthorizedClient(DomainType.Calendar)); }
        }

        public CalendarAPI(IRestClientFactory restClientFactory, IJsonSerializer jsonSerializer)
        {
            _restClientFactory = restClientFactory;
            _jsonSerializer = jsonSerializer;
        }

        public Domain.Calendar.Calendar GetCalendar(string calendarId)
        {
            var calendar = CalendarClient.Get("calendars/{0}".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<Domain.Calendar.Calendar>(calendar);
        }

        public IEnumerable<Event> GetEvents(string calendarId)
        {
            var events = CalendarClient.Get("calendars/{0}/events".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<CalendarEvents>(events).Items;
        }

        public void CreateEvent(string calendarId, Event @event)
        {
            CalendarClient.Post("calendars/{0}/events".FormatWith(calendarId), _jsonSerializer.Serialize(@event));
        }

        public void UpdateEvent(string calendarId, Event @event)
        {
            CalendarClient.Put("calendars/{0}/events/{1}".FormatWith(calendarId, @event.id), _jsonSerializer.Serialize(@event));
        }

        public void DeleteEvent(string calendarId, Event @event)
        {
            CalendarClient.Delete("calendars/{0}/events/{1}".FormatWith(calendarId, @event.id));
        }
    }
}