using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDependencyResolver>().To<DependencyResolver>();

            Bind<IDataAccessSettingsProvider>().To<SettingsProvider>();
        }
    }
}
