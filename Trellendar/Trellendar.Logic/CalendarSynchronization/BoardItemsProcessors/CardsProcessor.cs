using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.CalendarSynchronization.BoardItemsProcessors
{
    public class CardsProcessor : BoardItemsProcessorBase<Card>
    {
        private readonly ICardProcessor _cardProcessor;

        public CardsProcessor(UserContext userContext, ICalendarAPI calendarApi, ICardProcessor cardProcessor)
            : base(userContext, calendarApi)
        {
            _cardProcessor = cardProcessor;
        }

        protected override string GetItemID(Card item)
        {
            return item.Id;
        }

        protected override Event ProcessItem(Card item, string parentName)
        {
            return _cardProcessor.Process(item, parentName);
        }
    }
}
