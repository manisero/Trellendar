using System;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationAPI;
        private readonly ICalendarAPI _calendarAPI;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(ICalendarAuthorizationAPI calendarAuthorizationAPI, ICalendarAPI calendarAPI,
                           IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _calendarAPI = calendarAPI;
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
        }

        public User GetOrCreateUser(string authorizationCode, string authorizationRedirectUri)
        {
            var token = _calendarAuthorizationAPI.GetToken(authorizationCode, authorizationRedirectUri);
            var userInfo = _calendarAuthorizationAPI.GetUserInfo(token.IdToken);

            var userRepository = _repositoryFactory.Create<User>();
            var user = userRepository.GetSingleOrDefault(x => x.Email == userInfo.Email);

            if (user == null)
            {
                user = new User
                    {
                        Email = userInfo.Email,
                        CalendarAccessToken = token.AccessToken,
                        CalendarAccessTokenExpirationTS = token.GetExpirationTS(),
                        CalendarRefreshToken = token.RefreshToken,
                        LastSynchronizationTS = new DateTime(1900, 1, 1)
                    };

                userRepository.Add(user);
                _unitOfWork.SaveChanges();
            }

            return user;
        }

        public User GetUser(string userEmail)
        {
            return _repositoryFactory.Create<User>().GetSingleOrDefault(x => x.Email == userEmail);
        }

        public object GetAvailableCalendars()
        {
            return _calendarAPI.GetCalendars();
        }
    }
}