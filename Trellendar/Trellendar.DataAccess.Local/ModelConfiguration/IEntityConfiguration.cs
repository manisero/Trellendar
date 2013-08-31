using System.Data.Entity;

namespace Trellendar.DataAccess.Local.ModelConfiguration
{
    public interface IEntityConfiguration
    {
        void Configure(DbModelBuilder modelBuilder);
    }
}