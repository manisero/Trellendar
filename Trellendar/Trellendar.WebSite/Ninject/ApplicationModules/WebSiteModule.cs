using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;
using Trellendar.Logic.UserProfileSynchronization;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().To<SettingsProvider>();
            Bind<IUserProfileSynchronizaionSettingsProvider>().To<SettingsProvider>();

            // Ninject
            Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
        }
    }
}