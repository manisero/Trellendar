using System;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Trellendar;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class CalendarAccessTokenExpirationHandler : ICalendarAccessTokenExpirationHandler
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationAPI;
        private readonly TrellendarDataContext _dataContext;

        public CalendarAccessTokenExpirationHandler(ICalendarAuthorizationAPI calendarAuthorizationAPI, TrellendarDataContext dataContext)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _dataContext = dataContext;
        }

        public bool IsTokenExpired(User user)
        {
            return user.CalendarAccessTokenExpirationTS < DateTime.UtcNow;
        }

        public void RefreshToken(User user)
        {
            var newToken = _calendarAuthorizationAPI.GetNewToken(user.CalendarRefreshToken);

            user.CalendarAccessToken = newToken.Access_Token;
            user.CalendarAccessTokenExpirationTS = newToken.ExpirationTS;

            _dataContext.SaveChanges();
        }
    }
}
