using System.Linq;
using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.CalendarSynchronization._Impl
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;
        private readonly IUserProfileService _userProfileService;
        private readonly ICardProcessor _cardProcessor;

        private User User
        {
            get { return _userContext.User; }
        }

        public SynchronizationService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi, IUserProfileService userProfileService, ICardProcessor cardProcessor)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
            _userProfileService = userProfileService;
            _cardProcessor = cardProcessor;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(User.TrelloBoardID);

            if (board.Lists.IsNullOrEmpty() || board.Cards.IsNullOrEmpty())
            {
                return;
            }

            var calendarEvents = _calendarApi.GetEvents(User.CalendarID);

            _userProfileService.UpdateUserProfile(board, calendarEvents);

            foreach (var list in board.Lists)
            {
                var cards = board.Cards.Where(x => x.IdList == list.Id && x.DateLastActivity > User.LastSynchronizationTS);

                foreach (var card in cards)
                {
                    var existingEvent = calendarEvents.Items.SingleOrDefault(x => x.GetExtendedProperty(EventExtensions.SOURCE_ID_PROPERTY_KEY) == card.Id);
                    var newEvent = _cardProcessor.Process(card, list.Name);

                    if (newEvent == null)
                    {
                        if (existingEvent != null)
                        {
                            _calendarApi.DeleteEvent(User.CalendarID, existingEvent);
                        }

                        continue;
                    }

                    if (existingEvent == null)
                    {
                        _calendarApi.CreateEvent(User.CalendarID, newEvent);
                    }
                    else 
                    {
                        newEvent.id = existingEvent.id;
                        _calendarApi.UpdateEvent(User.CalendarID, newEvent);
                    }
                }
            }
        }
    }
}