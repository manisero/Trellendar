using System.Data.Entity;
using Trellendar.DataAccess.Local.ModelConfiguration;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Local
{
    public class TrellendarDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new ModelConfigurator().Configure(modelBuilder);
        }
    }
}
