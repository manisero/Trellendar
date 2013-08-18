using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.Service.Ninject.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().To<SettingsProvider>();
            Bind<IUserProfileSynchronizaionSettingsProvider>().To<SettingsProvider>();
            Bind<ITrellendarServiceSettingsProvider>().To<SettingsProvider>();

            // Ninject
            Bind<IDependencyResolver>().To<DependencyResolver>();
        }
    }
}
