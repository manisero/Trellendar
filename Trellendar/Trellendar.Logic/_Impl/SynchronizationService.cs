using Trellendar.Core.Extensions;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Trellendar;
using System.Linq;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Logic.Domain;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.Logic._Impl
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;
        private readonly IUserProfileService _userProfileService;
        private readonly ICalendarService _calendarService;

        private User User
        {
            get { return _userContext.User; }
        }

        public SynchronizationService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi, IUserProfileService userProfileService, ICalendarService calendarService)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
            _userProfileService = userProfileService;
            _calendarService = calendarService;
        }

        public void Synchronize()
        {
            var board = _trelloApi.GetBoard(User.TrelloBoardID);

            if (board.Lists.IsNullOrEmpty() || board.Cards.IsNullOrEmpty())
            {
                return;
            }

            var calendarEvents = _calendarApi.GetEvents(User.CalendarID);

            _userProfileService.UpdateUser(board, calendarEvents);
            _userProfileService.UpdateUserPreferences(board);
            _calendarService.UpdateCalendar(board, calendarEvents.Items.Where(x => x.IsGeneratedByTrellendar()));
        }
    }
}