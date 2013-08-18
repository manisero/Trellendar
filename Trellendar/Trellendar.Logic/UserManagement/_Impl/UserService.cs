using System;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Trellendar;
using System.Linq;
using Trellendar.Logic.Domain;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly ICalendarAuthorizationAPI _calendarAuthorizationAPI;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(ICalendarAuthorizationAPI calendarAuthorizationAPI, IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
        }

        public User GetOrCreateUser(string authorizationCode, string authorizationRedirectUri)
        {
            var token = _calendarAuthorizationAPI.GetToken(authorizationCode, authorizationRedirectUri);
            var userInfo = _calendarAuthorizationAPI.GetUserInfo(token.IdToken);

            var userRepository = _repositoryFactory.Create<User>();
            var user = userRepository.GetAll().SingleOrDefault(x => x.Email == userInfo.Email);

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
    }
}