using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Trellendar;

namespace Trellendar.WebSite.Nancy
{
    public class UserMapper : IUserMapper
    {
        private class UserIdentity : IUserIdentity
        {
            public string UserName { get; set; }

            public IEnumerable<string> Claims { get; set; }
        }

        private readonly IRepositoryFactory _repositoryFactory;

        public UserMapper(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = _repositoryFactory.Create<User>().GetSingleOrDefault(x => x.UserID == identifier);

            return user != null
                       ? new UserIdentity { UserName = user.Email }
                       : null;
        }
    }
}