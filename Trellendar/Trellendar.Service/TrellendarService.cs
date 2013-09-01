﻿using System;
using System.Data.Entity;
using System.ServiceProcess;
using System.Threading;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Migrations;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic;
using Ninject;
using Trellendar.Service.Ninject;

namespace Trellendar.Service
{
    public partial class Trellendar : ServiceBase
    {
        private IKernel _kernel;
        private Timer _timer;

        public Trellendar()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrellendarDataContext, Configuration>());

            _kernel = new NinjectBootstrapper().Bootstrap();

            var workInterval = _kernel.Get<ITrellendarServiceSettingsProvider>().WorkInterval;
            _timer = new Timer(DoWork, null, 0, workInterval);
        }

        protected override void OnStop()
        {
            _timer.Dispose();
        }

        private void DoWork(object state)
        {
            var userRepository = _kernel.Get<IRepositoryFactory>().Create<User>();

            foreach (var user in userRepository.GetAll())
            {
                _kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });

                var synchronizationTS = DateTime.UtcNow;

                foreach (var bond in user.BoardCalendarBonds)
                {
                    _kernel.Bind<BoardCalendarContext>().ToConstant(new BoardCalendarContext(bond));

                    var synchronizationService = _kernel.Get<ISynchronizationService>();
                    synchronizationService.Synchronize();

                    _kernel.Unbind<BoardCalendarContext>();
                }
                
                user.LastSynchronizationTS = synchronizationTS;
                _kernel.Get<IUnitOfWork>().SaveChanges();
                _kernel.Unbind<UserContext>();
            }
        }
    }
}
