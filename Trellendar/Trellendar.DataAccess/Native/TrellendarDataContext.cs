using System.Data.Entity;
using Trellendar.DataAccess.Native.ModelConfiguration;
using Trellendar.Domain.Native;

namespace Trellendar.DataAccess.Native
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
