using System.Data.Entity;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Session;
using Ninject;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Migrations;
using Trellendar.WebSite.AutoMapper;
using Trellendar.WebSite.Ninject;

namespace Trellendar.WebSite
{
    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            //new NinjectBootstrapper().RegisterApplicationModules(existingContainer);
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrellendarDataContext, Configuration>());
            CookieBasedSessions.Enable(pipelines);

            new AutoMapperBootstrapper().Bootstrap();
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            new NinjectBootstrapper().RegisterApplicationModules(container); // TODO: Move to ConfigureApplicationContainer and fix resolution issue
            new NinjectBootstrapper().RegisterRequestModules(container);
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            FormsAuthentication.Enable(pipelines, new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/LogIn",
                UserMapper = container.Get<IUserMapper>()
            });
        }
    }
}