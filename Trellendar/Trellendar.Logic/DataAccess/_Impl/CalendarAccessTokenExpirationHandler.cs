using System;
using Trellendar.DataAccess.Calendar;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Trellendar;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class CalendarAccessTokenExpirationHandler : ICalendarAccessTokenExpirationHandler
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationAPI;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessSettingsProvider _settingsProvider;

        public CalendarAccessTokenExpirationHandler(ICalendarAuthorizationAPI calendarAuthorizationAPI, IUnitOfWork unitOfWork, IDataAccessSettingsProvider settingsProvider)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _unitOfWork = unitOfWork;
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

            _unitOfWork.SaveChanges();
        }
    }
}
