using System.Data.Entity;
using Trellendar.DataAccess.Local.ModelConfiguration.Configurations;

namespace Trellendar.DataAccess.Local.ModelConfiguration
{
    public class ModelConfigurator
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            new UnregisteredUserConfiguration().Configure(modelBuilder);
            new UserConfiguration().Configure(modelBuilder);
            new UserPreferencesConfiguration().Configure(modelBuilder);
        }
    }
}
