using System.Data.Entity;
using Trellendar.DataAccess.Local.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local
{
    public class TrellendarDataContext : DbContext
    {
        public DbSet<UnregisteredUser> UnregisteredUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BoardCalendarBond> BoardCalendarBonds { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new ModelConfigurator().Configure(modelBuilder);
        }
    }
}
