using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CardSummaryFormatter : ISummaryFormatter<Card>
    {
        private readonly IParser<BoardItemName> _listNameParser;

        public CardSummaryFormatter(IParser<BoardItemName> listNameParser)
        {
            _listNameParser = listNameParser;
        }

        public string Format(Card entity, BoardCalendarBondSettings boardCalendarBondSettings)
        {
            if (entity.List == null || boardCalendarBondSettings == null || boardCalendarBondSettings.CardEventNameTemplate == null)
            {
                return entity.Name;
            }

            var listName = _listNameParser.Parse(entity.List.Name, boardCalendarBondSettings);

            return boardCalendarBondSettings.CardEventNameTemplate.FormatWith(listName != null ? listName.Value : null, entity.Name);
        }
    }
}