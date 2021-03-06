using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CardDescriptionFormatter : IDescriptionFormatter<Card>
    {
        private const string DESCRIPTION_FORMAT = "{0}\n\nLink: {1}";

        public string Format(Card entity, BoardCalendarBondSettings boardCalendarBondSettings)
        {
            return DESCRIPTION_FORMAT.FormatWith(entity.Description, entity.Url);
        }
    }
}