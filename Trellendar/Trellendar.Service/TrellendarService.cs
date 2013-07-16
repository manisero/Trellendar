using System.Data;
using System.ServiceProcess;
using Trellendar.DataAccess.Native;
using Trellendar.Logic;
using Ninject;
using System.Linq;
using Trellendar.Logic.CalendarSynchronization;
using Trellendar.Service.Ninject;

namespace Trellendar.Service
{
    public partial class Trellendar : ServiceBase
    {
        public Trellendar()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var kernel = new NinjectBootstrapper().Bootstrap();

            var dataContext = kernel.Get<TrellendarDataContext>();
            var user = dataContext.Users.FirstOrDefault();

            if (user == null)
            {
                throw new ObjectNotFoundException("No User found.");
            }

            kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });

            var synchronizationService = kernel.Get<ISynchronizationService>();
            synchronizationService.Synchronize();
        }

        protected override void OnStop()
        {
        }
    }
}
