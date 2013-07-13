using System.Collections.Generic;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarClient
    {
        string FormatRequestUri(string resource, IEnumerable<KeyValuePair<string, object>> parameters);

        string Get(string resource, IDictionary<string, object> parameters = null, bool includeAuthorizationParameters = true);

        string Post(string resource, IDictionary<string, object> parameters = null, bool includeAuthorizationParameters = true);
    }
}
