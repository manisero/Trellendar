using Ninject;
using Trellendar.WebSite.Ninject.ApplicationModules;

namespace Trellendar.WebSite.Ninject
{
    public class NinjectBootstrapper
    {
        public void RegisterApplicationModules(IKernel kernel)
        {
            kernel.Load(new CoreModule(), new DataAccessModule(), new LogicModule(), new WebSiteModule());
        }
    }
}