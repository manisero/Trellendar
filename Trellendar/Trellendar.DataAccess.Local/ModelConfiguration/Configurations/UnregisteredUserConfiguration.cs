using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class UnregisteredUserConfiguration : EntityConfigurationBase<UnregisteredUser>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<UnregisteredUser> entity)
        {
            entity.Property(x => x.UnregisteredUserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            entity.Property(x => x.Email).IsRequired().HasMaxLength(256);
            entity.Property(x => x.GoogleAccessToken).IsRequired().HasMaxLength(100);
            entity.Property(x => x.GoogleRefreshToken).IsRequired().HasMaxLength(100);
        }
    }
}
