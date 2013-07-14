﻿using System.Data;
using System.ServiceProcess;
using Trellendar.DataAccess;
using Trellendar.DataAccess.Trellendar;
using Trellendar.Logic;
using Trellendar.Service.Bootstrap;
using Ninject;
using System.Linq;

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
            var dataContext = new TrellendarDataContext();
            var user = dataContext.Users.FirstOrDefault();

            if (user == null)
            {
                throw new ObjectNotFoundException("No User found.");
            }

            var kernel = new NinjectBootstrapper().Bootstrap();
            kernel.Bind<UserContext>().ToConstant(new UserContext { User = user });

            var test = kernel.Get<Test>();
            test.TestTrello();
            test.TestCalendar();
        }

        protected override void OnStop()
        {
        }
    }
}
