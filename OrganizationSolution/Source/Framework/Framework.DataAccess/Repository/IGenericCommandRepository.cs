namespace Framework.DataAccess.Repository
{
    using Framework.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericCommandRepository<TDbContext, TEntity>
       where TDbContext : BaseDbContext<TDbContext>
       where TEntity : BaseEntity
    {
        Task<TEntity> Insert(TEntity entity);

        Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities);

        Task<TEntity> Update(TEntity entity);

        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

        Task<bool> Delete(long id);

        Task<bool> Delete(TEntity entity);

        Task<bool> Delete(IEnumerable<TEntity> entity);

        IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities);

        Task SaveOnly();

    }
}
