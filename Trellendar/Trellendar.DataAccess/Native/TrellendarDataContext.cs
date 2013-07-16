using System.Data.Entity;
using Trellendar.Domain.Native;

namespace Trellendar.DataAccess.Native
{
    public class TrellendarDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
