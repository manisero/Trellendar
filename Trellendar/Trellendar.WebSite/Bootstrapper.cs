using System.Data.Entity;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Session;
using Ninject;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Migrations;
using Trellendar.WebSite.Ninject;

namespace Trellendar.WebSite
{
    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            new NinjectBootstrapper().RegisterApplicationModules(existingContainer);
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrellendarDataContext, Configuration>());

            CookieBasedSessions.Enable(pipelines);
            FormsAuthentication.Enable(pipelines, new FormsAuthenticationConfiguration
                {
                    RedirectUrl = "~/LogIn",
                    UserMapper = container.Get<IUserMapper>()
                });

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            new NinjectBootstrapper().RegisterRequestModules(container);
        }
    }
}