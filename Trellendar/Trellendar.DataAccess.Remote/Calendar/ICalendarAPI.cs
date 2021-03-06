﻿using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.DataAccess.Remote.Calendar
{
    public interface ICalendarAPI
    {
        IEnumerable<Domain.Calendar.Calendar> GetCalendars();

        Domain.Calendar.Calendar GetCalendar(string calendarId);

        CalendarEvents GetEvents(string calendarId);

        void CreateEvent(string calendarId, Event @event);

        void UpdateEvent(string calendarId, Event @event);

        void DeleteEvent(string calendarId, Event @event);
    }
}
