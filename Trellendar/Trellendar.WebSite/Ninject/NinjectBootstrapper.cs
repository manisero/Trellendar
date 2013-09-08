using Ninject;

namespace Trellendar.WebSite.Ninject
{
    public class NinjectBootstrapper
    {
        public void RegisterApplicationModules(IKernel kernel)
        {
            kernel.Load(new ApplicationModules.CoreModule(),
                        new ApplicationModules.DataAccessModule(),
                        new ApplicationModules.LogicModule(),
                        new ApplicationModules.WebSiteModule());
        }

        public void RegisterRequestModules(IKernel kernel)
        {
            kernel.Load(new RequestModules.DataAccessModule(),
                        new RequestModules.WebSiteModule());
        }
    }
}