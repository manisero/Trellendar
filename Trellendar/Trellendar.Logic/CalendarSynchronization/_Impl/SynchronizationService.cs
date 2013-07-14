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
        private readonly ICalendarAPI _calendarApi;
        private readonly ICardProcessor _cardProcessor;

        public SynchronizationService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi, ICardProcessor cardProcessor)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
            _cardProcessor = cardProcessor;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(_userContext.User.TrelloBoardID);

            if (board.Lists.IsNullOrEmpty() || board.Cards.IsNullOrEmpty())
            {
                return;
            }

            var events = _calendarApi.GetEvents(_userContext.User.CalendarID);

            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id);

                foreach (var card in cards)
                {
                    var existingEvent = events.SingleOrDefault(x => x.SourceID == card.Id);
                    var newEvent = _cardProcessor.Process(card, list.Name);

                    if (newEvent == null)
                    {
                        if (existingEvent != null)
                        {
                            _calendarApi.DeleteEvent(_userContext.User.CalendarID, existingEvent);
                        }

                        continue;
                    }

                    if (existingEvent != null)
                    {
                        newEvent.Id = existingEvent.Id;
                        _calendarApi.UpdateEvent(_userContext.User.CalendarID, newEvent);
                    }
                    else 
                    {
                        _calendarApi.CreateEvent(_userContext.User.CalendarID, newEvent);
                    }
                }
            }
        }
    }
}