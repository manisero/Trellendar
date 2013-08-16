using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization.Formatters._Impl
{
    public class CheckItemSummaryFormatter : ICheckItemSummaryFormatter
    {
        private readonly IParser<BoardItemName> _checkListNameParser;

        public CheckItemSummaryFormatter(IParser<BoardItemName> checkListNameParser)
        {
            _checkListNameParser = checkListNameParser;
        }

        public string Format(CheckItem checkItem, UserPreferences userPreferences)
        {
            if (userPreferences == null)
            {
                return checkItem.Name;
            }

            string summary;

            if (checkItem.CheckList != null && userPreferences.CheckListEventNameTemplate != null)
            {
                var checkListName = _checkListNameParser.Parse(checkItem.CheckList.Name, userPreferences);
                summary = checkListName != null
                              ? userPreferences.CheckListEventNameTemplate.FormatWith(checkListName.Value, checkItem.Name)
                              : checkItem.Name;
            }
            else
            {
                summary = checkItem.Name;
            }

            if (checkItem.IsDone() && userPreferences.CheckListEventDoneSuffix != null)
            {
                summary += userPreferences.CheckListEventDoneSuffix;
            }

            return summary;
        }
    }
}