using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Native;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class UserConfiguration : EntityConfigurationBase<User>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<User> entity)
        {
            entity.Property(x => x.Email).IsRequired().HasMaxLength(256);
            entity.Property(x => x.TrelloBoardID).IsRequired().HasMaxLength(100);
            entity.Property(x => x.TrelloAccessToken).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CalendarID).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CalendarAccessToken).IsRequired().HasMaxLength(100);
            entity.Property(x => x.CalendarRefreshToken).IsRequired().HasMaxLength(100);
        }
    }
}
