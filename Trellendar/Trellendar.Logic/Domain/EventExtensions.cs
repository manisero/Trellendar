using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Domain
{
    public static class EventExtensions
    {
        public const string SOURCE_ID_PROPERTY_KEY = "sourceId";    

        public static string GetExtendedProperty(this Event @event, string key)
        {
            return @event.extendedProperties != null && @event.extendedProperties.@private != null && @event.extendedProperties.@private.ContainsKey(key)
                       ? @event.extendedProperties.@private[key]
                       : null;
        }
    }
}
