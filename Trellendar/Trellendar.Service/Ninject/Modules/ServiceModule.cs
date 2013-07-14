using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;

namespace Trellendar.Service.Ninject.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().To<SettingsProvider>();

            // Ninject
            Bind<IDependencyResolver>().To<DependencyResolver>();
        }
    }
}
