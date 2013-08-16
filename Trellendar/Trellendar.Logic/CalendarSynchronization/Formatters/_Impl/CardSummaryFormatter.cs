using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CardSummaryFormatter : ISummaryFormatter<Card>
    {
        private readonly IParser<BoardItemName> _listNameParser;

        public CardSummaryFormatter(IParser<BoardItemName> listNameParser)
        {
            _listNameParser = listNameParser;
        }

        public string Format(Card entity, UserPreferences userPreferences)
        {
            if (entity.List == null || userPreferences == null || userPreferences.CardEventNameTemplate == null)
            {
                return entity.Name;
            }

            var listName = _listNameParser.Parse(entity.List.Name, userPreferences);

            return userPreferences.CardEventNameTemplate.FormatWith(listName != null ? listName.Value : null, entity.Name);
        }
    }
}