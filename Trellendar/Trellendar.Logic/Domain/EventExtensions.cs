using System.Collections.Generic;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Domain
{
    public static class EventExtensions
    {
        public const string GENERATED_PROPERTY_KEY = "trellendar_generated";
        public const string SOURCE_ID_PROPERTY_KEY = "trellendar_sourceId";

        public static bool IsGeneratedByTrellendar(this Event @event)
        {
            return @event.ExtendedProperties != null &&
                   @event.ExtendedProperties.Private != null &&
                   @event.ExtendedProperties.Private.ContainsKey(GENERATED_PROPERTY_KEY);
        }

        public static string GetSourceID(this Event @event)
        {
            return @event.ExtendedProperties != null && @event.ExtendedProperties.Private != null && @event.ExtendedProperties.Private.ContainsKey(SOURCE_ID_PROPERTY_KEY)
                       ? @event.ExtendedProperties.Private[SOURCE_ID_PROPERTY_KEY]
                       : null;
        }

        public static EventExtendedProperties CreateExtendedProperties(string sourceId)
        {
            return new EventExtendedProperties
                {
                    Private = new Dictionary<string, string>
                        {
                            { GENERATED_PROPERTY_KEY, string.Empty },
                            { SOURCE_ID_PROPERTY_KEY, sourceId }
                        }
                };
        }
    }
}
