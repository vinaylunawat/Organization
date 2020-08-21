namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="GenericCommandRepository{TDbContext, TReadOnlyDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class GenericCommandRepository<TDbContext, TReadOnlyDbContext, TEntity> : RepositoryBase<TDbContext, TEntity>, IGenericCommandRepository<TDbContext, TEntity>
        where TEntity : BaseEntity
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Defines the _queryGenericRepository.
        /// </summary>
        private readonly IGenericQueryRepository<TReadOnlyDbContext, TEntity> _queryGenericRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommandRepository{TDbContext, TReadOnlyDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="TDbContext"/>.</param>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        protected GenericCommandRepository(TDbContext dbContext
            , IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository
            )
            : base(dbContext)
        {
            _dbContext = dbContext;
            _queryGenericRepository = queryGenericRepository;
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async virtual Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The UpdateOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        public virtual IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return entities;
        }

        /// <summary>
        /// The InsertOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async virtual Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
            return entities;
        }

        /// <summary>
        /// The SaveOnly.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SaveOnly()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async virtual Task<bool> Delete(long id)
        {
            var entity = await _queryGenericRepository.FetchByIdAsync(id).ConfigureAwait(false);
            _dbContext.Set<TEntity>().Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            int result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }
    }
}
