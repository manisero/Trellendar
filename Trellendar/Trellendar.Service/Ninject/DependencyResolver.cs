using Ninject;
using Trellendar.Core.DependencyResolution;

namespace Trellendar.Service.Ninject
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public DependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public TDependency Resolve<TDependency>()
        {
            return _kernel.Get<TDependency>();
        }
    }
}
