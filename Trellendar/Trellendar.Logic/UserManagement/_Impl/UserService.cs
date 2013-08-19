using System;
using System.Collections.Generic;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.Domain.Calendar;
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

        public bool TryGetUserID(string authorizationCode, string authorizationRedirectUri, out Guid userId)
        {
            var token = _calendarAuthorizationAPI.GetToken(authorizationCode, authorizationRedirectUri);
            var userInfo = _calendarAuthorizationAPI.GetUserInfo(token.IdToken);

            var user = GetUser(userInfo.Email);

            if (user != null)
            {
                userId = user.UserID;
                return true;
            }
            else
            {
                var newUser = new UnregisteredUser
                    {
                        Email = userInfo.Email,
                        GoogleAccessToken = token.AccessToken,
                        GoogleAccessTokenExpirationTS = token.GetExpirationTS(),
                        GoogleRefreshToken = token.RefreshToken,
                        CreateTS = DateTime.UtcNow
                    };

                _repositoryFactory.Create<UnregisteredUser>().Add(newUser);
                _unitOfWork.SaveChanges();

                userId = newUser.UnregisteredUserID;
                return false;
            }
        }

        public User GetUser(string userEmail)
        {
            return _repositoryFactory.Create<User>().GetSingleOrDefault(x => x.Email == userEmail);
        }

        public IList<Calendar> GetAvailableCalendars()
        {
            return _calendarAPI.GetCalendars();
        }
    }
}