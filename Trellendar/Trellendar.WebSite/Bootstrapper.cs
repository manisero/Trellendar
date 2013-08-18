using Nancy.Bootstrappers.Ninject;
using Ninject;
using Trellendar.WebSite.Ninject;

namespace Trellendar.WebSite
{
    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            new NinjectBootstrapper().RegisterApplicationModules(existingContainer);
        }
    }
}