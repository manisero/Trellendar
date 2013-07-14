using System;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic
{
    public class Test
    {
        private readonly ITrelloAPI _trelloAPI;
        private readonly ICalendarAPI _calendarAPI;
        private readonly UserContext _userContext;

        public Test(ITrelloAPI trelloAPI, ICalendarAPI calendarAPI, UserContext userContext)
        {
            _trelloAPI = trelloAPI;
            _calendarAPI = calendarAPI;
            _userContext = userContext;
        }

        public void TestTrello()
        {
            var board = _trelloAPI.GetBoard(_userContext.User.TrelloBoardID);
        }

        public void TestCalendar()
        {
            var calendar = _calendarAPI.GetCalendar(_userContext.User.CalendarID);
            var events = _calendarAPI.GetEvents(_userContext.User.CalendarID);

            _calendarAPI.CreateEvent(_userContext.User.CalendarID,
                                     new Event
                                         {
                                             Summary = "Test event 2",
                                             start = new TimeStamp(DateTime.Now),
                                             end = new TimeStamp(DateTime.Now.AddHours(3))
                                         });
        }
    }
}
