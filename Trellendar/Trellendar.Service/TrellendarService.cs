using System.ServiceProcess;

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
            new TrelloAPI().Test();
        }

        protected override void OnStop()
        {
        }
    }
}
