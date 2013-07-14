using Ninject;
using Trellendar.Service.Ninject.Modules;

namespace Trellendar.Service.Ninject
{
    public class NinjectBootstrapper
    {
        public IKernel Bootstrap()
        {
            return new StandardKernel(new CoreModule(), new DataAccessModule(), new LogicModule(), new ServiceModule());
        }
    }
}
