using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().To<SettingsProvider>();

            // Ninject
            Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
        }
    }
}