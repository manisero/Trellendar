using System.ServiceProcess;
using Trellendar.Service.Bootstrap;
using Ninject;

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

            var test = kernel.Get<Test>();
            test.TestTrello();
            test.TestCalendar();
        }

        protected override void OnStop()
        {
        }
    }
}
