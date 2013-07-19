namespace Trellendar.DataAccess.Native.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity : class;
    }
}
