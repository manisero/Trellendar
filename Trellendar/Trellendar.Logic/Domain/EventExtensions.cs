using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Domain
{
    public static class EventExtensions
    {
        public const string GENERATED_PROPERTY_KEY = "trellendar_generated";
        public const string SOURCE_ID_PROPERTY_KEY = "trellendar_sourceId";

        public static bool IsGeneratedByTrellendar(this Event @event)
        {
            return @event.extendedProperties != null &&
                   @event.extendedProperties.@private != null &&
                   @event.extendedProperties.@private.ContainsKey(GENERATED_PROPERTY_KEY);
        }

        public static string GetSourceID(this Event @event)
        {
            return @event.extendedProperties != null && @event.extendedProperties.@private != null && @event.extendedProperties.@private.ContainsKey(SOURCE_ID_PROPERTY_KEY)
                       ? @event.extendedProperties.@private[SOURCE_ID_PROPERTY_KEY]
                       : null;
        }
    }
}
