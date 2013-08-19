using Ninject;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic;
using Trellendar.WebSite.Logic;

namespace Trellendar.WebSite.Ninject
{
    public class NinjectUserContextRegistrar : IUserContextRegistrar
    {
        private readonly IKernel _kernel;

        public NinjectUserContextRegistrar(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Register(string userEmail)
        {
            _kernel.Unbind<UserContext>();

            var user = _kernel.Get<IRepositoryFactory>().Create<User>().GetSingleOrDefault(x => x.Email == userEmail);

            if (user != null)
            {
                _kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });
            }
        }
    }
}