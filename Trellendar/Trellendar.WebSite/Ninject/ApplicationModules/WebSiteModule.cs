using Nancy.Authentication.Forms;
using Ninject.Modules;
using Trellendar.Logic.DataAccess;
using Trellendar.WebSite.Logic;
using Trellendar.WebSite.Logic._Impl;
using Trellendar.WebSite.Nancy;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    public class WebSiteModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessSettingsProvider>().ToConstant(new SettingsProvider());

            // Logic
            Bind<ILogInService>().To<LogInService>();

            // Nancy
            Bind<IUserMapper>().To<UserMapper>();

            // Ninject
            Bind<IUserContextRegistrar>().To<NinjectUserContextRegistrar>();
        }
    }
}