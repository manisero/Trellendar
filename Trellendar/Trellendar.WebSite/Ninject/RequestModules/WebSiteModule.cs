using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.WebSite.Logic;
using Trellendar.WebSite.Logic._Impl;

namespace Trellendar.WebSite.Ninject.RequestModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            // Logic
            Bind<ILogInService>().To<LogInService>();

            // Ninject
            Rebind<IDependencyResolver>().ToConstant(new NinjectDependencyResolver(Kernel));
        }
    }
}