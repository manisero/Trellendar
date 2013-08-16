using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardEventDescriptionFormatter : ICardEventDescriptionFormatter
    {
        private const string EVENT_DESCRIPTION_FORMAT = "{0}\n\nLink: {1}";

        public string Format(Card card)
        {
            return EVENT_DESCRIPTION_FORMAT.FormatWith(card.Description, card.Url);
        }
    }
}