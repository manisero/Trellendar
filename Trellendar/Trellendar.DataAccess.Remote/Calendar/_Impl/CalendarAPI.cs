﻿using System.Collections.Generic;
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

        public IEnumerable<Domain.Calendar.Calendar> GetCalendars()
        {
            var calendarsJson = CalendarClient.Get("users/me/calendarList");
            var calendarList = _jsonSerializer.Deserialize<CalendarList>(calendarsJson);

            return calendarList != null ? calendarList.Items : null;
        }

        public Domain.Calendar.Calendar GetCalendar(string calendarId)
        {
            var calendarJson = CalendarClient.Get("calendars/{0}".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<Domain.Calendar.Calendar>(calendarJson);
        }

        public CalendarEvents GetEvents(string calendarId)
        {
            var eventsJson = CalendarClient.Get("calendars/{0}/events".FormatWith(calendarId));

            return _jsonSerializer.Deserialize<CalendarEvents>(eventsJson);
        }

        public void CreateEvent(string calendarId, Event @event)
        {
            CalendarClient.Post("calendars/{0}/events".FormatWith(calendarId), _jsonSerializer.Serialize(@event));
        }

        public void UpdateEvent(string calendarId, Event @event)
        {
            CalendarClient.Put("calendars/{0}/events/{1}".FormatWith(calendarId, @event.Id), _jsonSerializer.Serialize(@event));
        }

        public void DeleteEvent(string calendarId, Event @event)
        {
            CalendarClient.Delete("calendars/{0}/events/{1}".FormatWith(calendarId, @event.Id));
        }
    }
}