using System.Collections.Generic;

namespace Trellendar.DataAccess.Calendar
{
    public interface ICalendarClient
    {
        string Get(string resource, IDictionary<string, object> parameters = null);
    }
}
