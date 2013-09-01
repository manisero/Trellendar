using System.Data.Entity;
using System.Linq;
using Trellendar.Core.Extensions;

namespace Trellendar.DataAccess.Local.ModelConfiguration
{
    public class ModelConfigurator
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            var configurationTypes = GetType().Assembly.GetConcreteTypes<IEntityConfiguration>();

            configurationTypes.Select(x => x.CreateInstance<IEntityConfiguration>())
                              .ForEach(x => x.Configure(modelBuilder));
        }
    }
}
