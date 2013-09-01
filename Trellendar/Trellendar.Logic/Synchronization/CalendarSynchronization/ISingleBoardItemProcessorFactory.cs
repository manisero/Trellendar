namespace Trellendar.Logic.Synchronization.CalendarSynchronization
{
    public interface ISingleBoardItemProcessorFactory
    {
        ISingleBoardItemProcessor<TItem> Create<TItem>();
    }
}
