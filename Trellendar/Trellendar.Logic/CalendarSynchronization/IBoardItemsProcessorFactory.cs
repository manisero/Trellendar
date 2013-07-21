namespace Trellendar.Logic.CalendarSynchronization
{
    public interface IBoardItemsProcessorFactory
    {
        IBoardItemsProcessor<TItem> Create<TItem>();
    }
}
