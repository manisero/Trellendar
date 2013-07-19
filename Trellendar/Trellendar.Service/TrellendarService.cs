using System.ServiceProcess;
using Trellendar.DataAccess.Native.Repository;
using Trellendar.Domain.Native;
using Trellendar.Logic;
using Ninject;
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

            var userRepository = kernel.Get<IRepositoryFactory>().Create<User>();

            foreach (var user in userRepository.GetAll())
            {
                kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });

                var synchronizationService = kernel.Get<ISynchronizationService>();
                synchronizationService.Synchronize();
            }
        }

        protected override void OnStop()
        {
        }
    }
}
