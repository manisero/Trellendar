namespace Trellendar.DataAccess.Local.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity : class;
    }
}
