using System.Data.Entity;
using Trellendar.DataAccess.Native.ModelConfiguration.Configurations;

namespace Trellendar.DataAccess.Native.ModelConfiguration
{
    public class ModelConfigurator
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder);
        }
    }
}
