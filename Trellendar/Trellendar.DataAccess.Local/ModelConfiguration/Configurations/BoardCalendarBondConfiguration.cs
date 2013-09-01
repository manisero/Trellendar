using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class BoardCalendarBondConfiguration : EntityConfigurationBase<BoardCalendarBond>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<BoardCalendarBond> entity)
        {
            entity.HasKey(x => new { x.UserID, x.BoardID, x.CalendarID });

            entity.Property(x => x.BoardID).HasMaxLength(100);
            entity.Property(x => x.CalendarID).HasMaxLength(100);
            entity.Property(x => x.CalendarTimeZone).HasMaxLength(50);
        }
    }
}
