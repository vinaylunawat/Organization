namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class GenericCommandRepository<TDbContext, TReadOnlyDbContext, TEntity> : RepositoryBase<TDbContext, TEntity>, IGenericCommandRepository<TDbContext, TEntity>
        where TEntity : BaseEntity
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
    {
        private readonly TDbContext _dbContext;
        private readonly IGenericQueryRepository<TReadOnlyDbContext, TEntity> _queryGenericRepository;

        protected GenericCommandRepository(TDbContext dbContext
            , IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository
            )
            : base(dbContext)
        {
            _dbContext = dbContext;
            _queryGenericRepository = queryGenericRepository;
        }

        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async virtual Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        public async virtual Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async virtual Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {            
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        public virtual IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return entities;
        }

        public async virtual Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            return entities;
        }

        public async Task SaveOnly()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async virtual Task<bool> Delete(long id)
        {
            var entity = await _queryGenericRepository.FetchByIdAsync(id).ConfigureAwait(false);
            _dbContext.Set<TEntity>().Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        public async Task<bool> Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }
    }
}
