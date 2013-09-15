using System;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions;

namespace Trellendar.Logic.UserManagement._Impl
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegistrationService(IRepositoryFactory repositoryFactory, IUnitOfWork unitOfWork)
        {
            _repositoryFactory = repositoryFactory;
            _unitOfWork = unitOfWork;
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
                    GoogleAccessToken = unregisteredUser.GoogleAccessToken,
                    GoogleAccessTokenExpirationTS = unregisteredUser.GoogleAccessTokenExpirationTS,
                    GoogleRefreshToken = unregisteredUser.GoogleRefreshToken,
                    DefaultBondSettings = BoardCalendarBondSettings.CreateDefault(DateTime.UtcNow),
					CreateTS = DateTime.UtcNow
                };

            _repositoryFactory.Create<User>().Add(user);
            unregisteredUserRepository.Remove(unregisteredUser);
            _unitOfWork.SaveChanges();

            return user.UserID;
        }
    }
}