using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Trellendar.DataAccess.Local.Repository
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        IReadOnlyCollection<TEntity> GetAll();

        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        IReadOnlyCollection<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
