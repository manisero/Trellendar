using System;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Google;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.DataAccess._Impl
{
    public class CalendarAccessTokenExpirationHandler : ICalendarAccessTokenExpirationHandler
    {
        private readonly IGoogleAuthorizationAPI _calendarAuthorizationAPI;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessSettingsProvider _settingsProvider;

        public CalendarAccessTokenExpirationHandler(IGoogleAuthorizationAPI calendarAuthorizationAPI, IUnitOfWork unitOfWork, IDataAccessSettingsProvider settingsProvider)
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

            user.CalendarAccessToken = newToken.AccessToken;
            user.CalendarAccessTokenExpirationTS = newToken.GetExpirationTS();

            _unitOfWork.SaveChanges();
        }
    }
}
