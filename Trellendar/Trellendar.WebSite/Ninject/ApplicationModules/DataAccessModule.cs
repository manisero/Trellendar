using System.Data.Entity;
using Ninject.Modules;
using Trellendar.DataAccess.Local;
using Trellendar.DataAccess.Local.Repository;
using Trellendar.DataAccess.Local.Repository._Impl;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            // Local // NOTE: These are only for application-scope operations (e.g. User mapping - see UserMapper)
            Bind<DbContext>().ToConstant(new TrellendarDataContext());
            Bind<IUnitOfWork>().To<EntityFrameworkUnitOfWork>();
            Bind<IRepositoryFactory>().To<EntityFrameworkRepositoryFactory>();
        }
    }
}
