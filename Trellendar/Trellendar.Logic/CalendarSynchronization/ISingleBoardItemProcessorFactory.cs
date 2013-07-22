namespace Trellendar.Logic.CalendarSynchronization
{
    public interface ISingleBoardItemProcessorFactory
    {
        ISingleBoardItemProcessor<TItem> Create<TItem>();
    }
}
