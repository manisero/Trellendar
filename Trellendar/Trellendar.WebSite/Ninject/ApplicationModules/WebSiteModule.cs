using Nancy.Authentication.Forms;
using Ninject.Modules;
using Trellendar.Core.DependencyResolution;
using Trellendar.Logic.DataAccess;
using Trellendar.WebSite.Logic;
using Trellendar.WebSite.Nancy;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().ToConstant(new SettingsProvider());

            // Nancy
            Bind<IUserMapper>().To<UserMapper>();

            // Ninject
            Bind<IDependencyResolver>().ToConstant(new NinjectDependencyResolver(Kernel));
            Bind<IUserContextRegistrar>().To<NinjectUserContextRegistrar>();
        }
    }
}