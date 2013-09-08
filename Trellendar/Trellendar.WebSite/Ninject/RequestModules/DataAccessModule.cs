using System.Data.Entity;
using Ninject.Modules;
using Trellendar.DataAccess.Local;

namespace Trellendar.WebSite.Ninject.RequestModules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            // Local
            Rebind<DbContext>().ToConstant(new TrellendarDataContext());
        }
    }
}
