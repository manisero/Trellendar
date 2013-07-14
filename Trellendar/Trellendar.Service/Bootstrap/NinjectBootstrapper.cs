using Ninject;
using Trellendar.Service.Bootstrap.NinjectModules;

namespace Trellendar.Service.Bootstrap
{
    public class NinjectBootstrapper
    {
        public IKernel Bootstrap()
        {
            return new StandardKernel(new CoreModule(), new DataAccessModule(), new LogicModule(), new ServiceModule());
        }
    }
}
