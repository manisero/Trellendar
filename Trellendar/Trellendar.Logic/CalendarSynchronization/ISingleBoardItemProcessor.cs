using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.CalendarSynchronization
{
    public interface ISingleBoardItemProcessor<TItem>
    {
        string GetItemID(TItem item);

        Event Process(TItem item, string parentName);
    }
}
