using System;
using System.Collections.Generic;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Google;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly IGoogleAuthorizationAPI _calendarAuthorizationAPI;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarAPI;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IGoogleAuthorizationAPI calendarAuthorizationAPI, ITrelloAPI trelloApi, ICalendarAPI calendarAPI,
                           IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork)
        {
            _calendarAuthorizationAPI = calendarAuthorizationAPI;
            _trelloApi = trelloApi;
            _calendarAPI = calendarAPI;
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
        }

        public bool TryGetUserID(string authorizationCode, string authorizationRedirectUri, out Guid userId)
        {
            var token = _calendarAuthorizationAPI.GetToken(authorizationCode, authorizationRedirectUri);
            var user = _repositoryFactory.Create<User>().GetSingleOrDefault(x => x.Email == token.UserEmail);

            if (user != null)
            {
                userId = user.UserID;
                return true;
            }
            else
            {
                var newUser = new UnregisteredUser
                    {
                        Email = token.UserEmail,
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

        public Guid RegisterUser(Guid unregisteredUserId, string trelloAccessToken)
        {
            var unregisteredUserRepository = _repositoryFactory.Create<UnregisteredUser>();
            var unregisteredUser = unregisteredUserRepository.GetSingleOrDefault(x => x.UnregisteredUserID == unregisteredUserId);

            if (unregisteredUser == null)
            {
                throw new InvalidOperationException("Unregistered User of ID '{0}' does not exist in the database".FormatWith(unregisteredUserId));
            }

            var user = new User
                {
                    Email = unregisteredUser.Email,
                    TrelloAccessToken = trelloAccessToken,
                    BoardID = "TODO",
                    GoogleAccessToken = unregisteredUser.GoogleAccessToken,
                    GoogleAccessTokenExpirationTS = unregisteredUser.GoogleAccessTokenExpirationTS,
                    GoogleRefreshToken = unregisteredUser.GoogleRefreshToken,
                    CalendarID = "TODO",
                    LastSynchronizationTS = new DateTime(1900, 1, 1)
                };

            _repositoryFactory.Create<User>().Add(user);
            unregisteredUserRepository.Remove(unregisteredUser);
            _unitOfWork.SaveChanges();

            return user.UserID;
        }

        public IEnumerable<Board> GetAvailableBoards()
        {
            return _trelloApi.GetBoards();
        }

        public IEnumerable<Calendar> GetAvailableCalendars()
        {
            return _calendarAPI.GetCalendars();
        }
    }
}