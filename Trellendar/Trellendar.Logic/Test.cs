using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trello;

namespace Trellendar
{
    public class Test
    {
        private readonly ITrelloAPI _trelloAPI;
        private readonly ICalendarAPI _calendarAPI;

        public Test(ITrelloAPI trelloAPI, ICalendarAPI calendarAPI)
        {
            _trelloAPI = trelloAPI;
            _calendarAPI = calendarAPI;
        }

        public void TestTrello()
        {
            //var board = _trelloAPI.GetBoard("51e072d0f1171f9b1e002b48");
        }

        public void TestCalendar()
        {
            //var authorizationUri = _calendarAPI.GetAuthorizationUri();
            //var token = _calendarAPI.GetToken("confidential");
            //var newToken = _calendarAPI.GetNewToken(token.Refresh_Token);
            var calendar = _calendarAPI.GetCalendar("5u9ci4r27ortoec3srd1nn264c@group.calendar.google.com");
        }
    }
}
