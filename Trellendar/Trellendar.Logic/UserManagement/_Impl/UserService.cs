using System.Collections.Generic;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trello;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarApi;

        public UserService(UserContext userContext, ITrelloAPI trelloApi, ICalendarAPI calendarApi)
        {
            _userContext = userContext;
            _trelloApi = trelloApi;
            _calendarApi = calendarApi;
        }

        public IEnumerable<Board> GetAvailableBoards()
        {
            return _trelloApi.GetBoards();
        }

        public IEnumerable<Calendar> GetAvailableCalendars()
        {
            return _calendarApi.GetCalendars();
        }

        public void UpdateBoardCalendarBonds(IDictionary<string, string> bonds)
        {

        }
    }
}