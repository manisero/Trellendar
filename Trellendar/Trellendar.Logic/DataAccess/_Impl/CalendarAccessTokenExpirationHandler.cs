using System;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Native;
using Trellendar.Domain.Native;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class CalendarAccessTokenExpirationHandler : ICalendarAccessTokenExpirationHandler
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationAPI;
        private readonly TrellendarDataContext _dataContext;
        private readonly IDataAccessSettingsProvider _settingsProvider;

        public CalendarAccessTokenExpirationHandler(ICalendarAuthorizationAPI calendarAuthorizationAPI, TrellendarDataContext dataContext, IDataAccessSettingsProvider settingsProvider)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _dataContext = dataContext;
            _settingsProvider = settingsProvider;
        }

        public bool IsTokenExpired(User user)
        {
            return user.CalendarAccessTokenExpirationTS < DateTime.UtcNow.AddSeconds(_settingsProvider.CalendarAccessTokenExpirationReserve);
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
