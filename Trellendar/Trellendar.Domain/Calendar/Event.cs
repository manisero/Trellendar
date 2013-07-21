using System.Collections.Generic;

namespace Trellendar.Domain.Calendar
{
    public class Event
    {
        public string id { get; set; }

        public string summary { get; set; }

        public TimeStamp start { get; set; }

        public TimeStamp end { get; set; }

        public EventExtendedProperties extendedProperties { get; set; }
    }

    public class EventExtendedProperties
    {
        public IDictionary<string, string> @private { get; set; }

        public IDictionary<string, string> shared { get; set; }
    }
}
