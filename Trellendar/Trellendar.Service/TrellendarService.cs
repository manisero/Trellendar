using System.ServiceProcess;
using Trellendar.Trello._Impl;

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
            new TrelloAPI(new TrelloClient()).Test();
        }

        protected override void OnStop()
        {
        }
    }
}
