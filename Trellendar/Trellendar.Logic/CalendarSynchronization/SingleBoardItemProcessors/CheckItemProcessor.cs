using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.SingleBoardItemProcessors
{
    public class CheckItemProcessor : ISingleBoardItemProcessor<CheckItem>
    {
        public string GetItemID(CheckItem item)
        {
            return item.Id;
        }

        public Event Process(CheckItem item, string parentName)
        {
            throw new System.NotImplementedException();
        }
    }
}
