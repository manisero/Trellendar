using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemSummaryFormatter : ISummaryFormatter<CheckItem>
    {
        private readonly IParser<BoardItemName> _checkListNameParser;

        public CheckItemSummaryFormatter(IParser<BoardItemName> checkListNameParser)
        {
            _checkListNameParser = checkListNameParser;
        }

        public string Format(CheckItem entity, UserPreferences userPreferences)
        {
            if (userPreferences == null)
            {
                return entity.Name;
            }

            string summary;

            if (entity.CheckList != null && userPreferences.CheckListEventNameTemplate != null)
            {
                var checkListName = _checkListNameParser.Parse(entity.CheckList.Name, userPreferences);
                summary = checkListName != null
                              ? userPreferences.CheckListEventNameTemplate.FormatWith(checkListName.Value, entity.Name)
                              : entity.Name;
            }
            else
            {
                summary = entity.Name;
            }

            if (entity.IsDone() && userPreferences.CheckListEventDoneSuffix != null)
            {
                summary += userPreferences.CheckListEventDoneSuffix;
            }

            return summary;
        }
    }
}