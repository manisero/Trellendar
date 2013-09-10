using Ninject.Modules;
using Trellendar.Core.DependencyResolution;

namespace Trellendar.WebSite.Ninject.RequestModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            // Ninject
            Rebind<IDependencyResolver>().ToConstant(new NinjectDependencyResolver(Kernel));
        }
    }
}