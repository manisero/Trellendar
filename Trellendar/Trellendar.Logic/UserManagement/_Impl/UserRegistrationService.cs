using System;
using Trellendar.Core.Extensions.AutoMapper;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trellendar;
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
            var unregisteredUser = token.MapTo<UnregisteredUser>();
            unregisteredUser.CreateTS = DateTime.UtcNow;

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

            var user = unregisteredUser.MapTo<User>();
            user.TrelloAccessToken = trelloAccessToken;
            user.DefaultBondSettings = BoardCalendarBondSettings.CreateDefault(DateTime.UtcNow);
            user.CreateTS = DateTime.UtcNow;

            _repositoryFactory.Create<User>().Add(user);
            unregisteredUserRepository.Remove(unregisteredUser);
            _unitOfWork.SaveChanges();

            return user.UserID;
        }
    }
}