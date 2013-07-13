﻿using System.Collections.Generic;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarClient
    {
        string FormatRequestUri(string resource, IDictionary<string, object> parameters);

        string Get(string resource, IDictionary<string, object> parameters = null);

        string Post(string resource, string content);
        string Post(string resource, IDictionary<string, object> parameters = null);
    }
}
