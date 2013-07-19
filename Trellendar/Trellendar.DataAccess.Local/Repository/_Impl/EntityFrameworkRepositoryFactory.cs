using System.Data.Entity;
using Trellendar.Core.DependencyResolution;

namespace Trellendar.DataAccess.Local.Repository._Impl
{
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        public EntityFrameworkRepositoryFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IRepository<TEntity> Create<TEntity>() where TEntity : class
        {
            return new EntityFrameworkRepository<TEntity>(_dependencyResolver.Resolve<DbContext>());
        }
    }
}
