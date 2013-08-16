using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardEventSummaryFormatter : ICardEventSummaryFormatter
    {
        private readonly IParser<BoardItemName> _listNameParser;

        public CardEventSummaryFormatter(IParser<BoardItemName> listNameParser)
        {
            _listNameParser = listNameParser;
        }

        public string Format(Card card, UserPreferences userPreferences)
        {
            if (card.List == null || userPreferences == null || userPreferences.CardEventNameTemplate == null)
            {
                return card.Name;
            }

            var listName = _listNameParser.Parse(card.List.Name, userPreferences);

            return userPreferences.CardEventNameTemplate.FormatWith(listName != null ? listName.Value : null, card.Name);
        }
    }
}