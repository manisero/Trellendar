using System.Collections.Generic;

namespace Trellendar.DataAccess.Local.Repository
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        IReadOnlyCollection<TEntity> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
