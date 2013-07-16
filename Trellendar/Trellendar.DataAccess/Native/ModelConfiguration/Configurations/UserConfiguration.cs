using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Native;

namespace Trellendar.DataAccess.Native.ModelConfiguration.Configurations
{
    public class UserConfiguration : EntityConfigurationBase<User>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<User> entity)
        {
            
        }
    }
}
