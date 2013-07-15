using System.Collections.Generic;

namespace Trellendar.Domain.Calendar
{
    public class Event
    {
        public const string SOURCE_ID_PROPERTY_KEY = "sourceId";

        public string id { get; set; }

        public string summary { get; set; }

        public TimeStamp start { get; set; }

        public TimeStamp end { get; set; }

        public EventExtendedProperties extendedProperties { get; set; }

        public string SourceID
        {
            get
            {
                return extendedProperties != null && extendedProperties.@private != null &&
                       extendedProperties.@private.ContainsKey(SOURCE_ID_PROPERTY_KEY)
                           ? extendedProperties.@private[SOURCE_ID_PROPERTY_KEY]
                           : null;
            }
        }
    }

    public class EventExtendedProperties
    {
        public IDictionary<string, string> @private { get; set; }

        public IDictionary<string, string> shared { get; set; }
    }
}
