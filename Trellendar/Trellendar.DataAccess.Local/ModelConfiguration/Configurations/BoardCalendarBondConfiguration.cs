using System.Data.Entity.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local.ModelConfiguration.Configurations
{
    public class BoardCalendarBondConfiguration : EntityConfigurationBase<BoardCalendarBond>
    {
        protected override void ConfigureEntity(EntityTypeConfiguration<BoardCalendarBond> entity)
        {
            entity.Property(x => x.CalendarTimeZone).HasMaxLength(50);
        }
    }
}
