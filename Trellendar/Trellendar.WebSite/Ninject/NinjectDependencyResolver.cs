using Ninject;
using Trellendar.Core.DependencyResolution;

namespace Trellendar.WebSite.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public TDependency Resolve<TDependency>()
        {
            return _kernel.Get<TDependency>();
        }
    }
}