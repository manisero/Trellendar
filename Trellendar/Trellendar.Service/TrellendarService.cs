using System.ServiceProcess;
using Trellendar.Core.Serialization._Impl;
using Trellendar.DataAccess.Trello._Impl;

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
            new TrelloTest(new TrelloAPI(new TrelloClient(), new JsonSerializer())).Test();
        }

        protected override void OnStop()
        {
        }
    }
}
