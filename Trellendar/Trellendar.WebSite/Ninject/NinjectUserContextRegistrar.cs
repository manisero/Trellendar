using Ninject;
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

        public void Register(User user)
        {
            _kernel.Unbind<UserContext>();

            if (user != null)
            {
                _kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });
            }
        }
    }
}