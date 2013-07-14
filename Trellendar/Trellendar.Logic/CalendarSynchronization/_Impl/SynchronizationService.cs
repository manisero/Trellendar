using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;
using System.Linq;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICardProcessor _cardProcessor;
        private readonly ICalendarAPI _calendarApi;

        public SynchronizationService(UserContext userContext, ITrelloAPI trelloApi, ICardProcessor cardProcessor, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _cardProcessor = cardProcessor;
            _calendarApi = calendarApi;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(_userContext.User.TrelloBoardID);

            if (board.Lists.IsNullOrEmpty() || board.Cards.IsNullOrEmpty())
            {
                return;
            }

            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id);

                foreach (var card in cards)
                {
                    var events = _cardProcessor.Process(card, list.Name);

                    foreach (var @event in events)
                    {
                        _calendarApi.CreateEvent(_userContext.User.CalendarID, @event);
                    }
                }
            }
        }
    }
}