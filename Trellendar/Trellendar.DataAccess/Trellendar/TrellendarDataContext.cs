using System.Data.Entity;
using Trellendar.Domain.Trellendar;

namespace Trellendar.DataAccess.Trellendar
{
    public class TrellendarDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
