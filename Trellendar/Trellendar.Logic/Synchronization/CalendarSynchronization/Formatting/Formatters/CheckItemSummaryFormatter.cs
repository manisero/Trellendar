using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization.Formatting.Formatters
{
    public class CheckItemSummaryFormatter : ISummaryFormatter<CheckItem>
    {
        private readonly IParser<BoardItemName> _checkListNameParser;

        public CheckItemSummaryFormatter(IParser<BoardItemName> checkListNameParser)
        {
            _checkListNameParser = checkListNameParser;
        }

        public string Format(CheckItem entity, BoardCalendarBondSettings boardCalendarBondSettings)
        {
            if (boardCalendarBondSettings == null)
            {
                return entity.Name;
            }

            string summary;

            if (entity.CheckList != null && boardCalendarBondSettings.CheckListEventNameTemplate != null)
            {
                var checkListName = _checkListNameParser.Parse(entity.CheckList.Name, boardCalendarBondSettings);
                summary = checkListName != null
                              ? boardCalendarBondSettings.CheckListEventNameTemplate.FormatWith(checkListName.Value, entity.Name)
                              : entity.Name;
            }
            else
            {
                summary = entity.Name;
            }

            if (entity.IsDone() && boardCalendarBondSettings.CheckListEventDoneSuffix != null)
            {
                summary += boardCalendarBondSettings.CheckListEventDoneSuffix;
            }

            return summary;
        }
    }
}