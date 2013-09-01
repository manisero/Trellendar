using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Synchronization.CalendarSynchronization
{
    public interface ISingleBoardItemProcessor<TItem>
    {
        string GetItemID(TItem item);

        Event Process(TItem item);
    }
}
