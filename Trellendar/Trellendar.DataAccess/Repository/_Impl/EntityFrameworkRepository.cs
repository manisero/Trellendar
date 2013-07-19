using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Trellendar.DataAccess.Repository._Impl
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        private DbSet<TEntity> EntitySet
        {
            get { return _context.Set<TEntity>(); }
        }

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            return EntitySet.ToList();
        }

        public void Add(TEntity entity)
        {
            EntitySet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entityEntry = _context.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                EntitySet.Attach(entity);
                entityEntry.State = EntityState.Modified;
            }
        }

        public void Remove(TEntity entity)
        {
            EntitySet.Remove(entity);
        }
    }
}
