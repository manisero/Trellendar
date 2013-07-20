using System;
using System.ServiceProcess;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Trellendar;
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
                var synchronizationTs = DateTime.UtcNow;
                kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });

                var synchronizationService = kernel.Get<ISynchronizationService>();
                synchronizationService.Synchronize();

                user.LastSynchronizationTS = synchronizationTs;
                kernel.Get<IUnitOfWork>().SaveChanges();
            }
        }

        protected override void OnStop()
        {
        }
    }
}
