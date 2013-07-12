using Ninject.Modules;
using Trellendar.DataAccess.Trello;
using Trellendar.DataAccess.Trello._Impl;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrelloClient>().To<TrelloClient>();
            Bind<ITrelloAPI>().To<TrelloAPI>();
        }
    }
}
