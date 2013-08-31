using System;
using System.Collections.Generic;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Remote.Calendar;
using Trellendar.DataAccess.Remote.Trello;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrelloAPI _trelloApi;
        private readonly ICalendarAPI _calendarAPI;

        public UserService(IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork, ITrelloAPI trelloApi, ICalendarAPI calendarAPI)
        {
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
            _trelloApi = trelloApi;
            _calendarAPI = calendarAPI;
        }

        public bool TryGetUserID(string userEmail, out Guid userId)
        {
            var user = _repositoryFactory.Create<User>().GetSingleOrDefault(x => x.Email == userEmail);

            if (user == null)
            {
                userId = new Guid();
                return false;
            }
            
            userId = user.UserID;
            return true;
        }

        public bool TryCreateUnregisteredUser(Token token, out Guid unregisteredUserId)
        {
            var unregisteredUser = new UnregisteredUser
            {
                Email = token.UserEmail,
                GoogleAccessToken = token.AccessToken,
                GoogleAccessTokenExpirationTS = token.GetExpirationTS(),
                GoogleRefreshToken = token.RefreshToken,
                CreateTS = DateTime.UtcNow
            };

            try
            {
                _repositoryFactory.Create<UnregisteredUser>().Add(unregisteredUser);
                _unitOfWork.SaveChanges();

                unregisteredUserId = unregisteredUser.UnregisteredUserID;
                return true;
            }
            catch (Exception exception)
            {
                unregisteredUserId = new Guid();
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