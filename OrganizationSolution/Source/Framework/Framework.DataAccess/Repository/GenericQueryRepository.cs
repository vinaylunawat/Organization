namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Framework.Service.Extension;
    using Framework.Service.Utilities.Criteria;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="GenericQueryRepository{TDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class GenericQueryRepository<TDbContext, TEntity> : RepositoryBase<TDbContext, TEntity>, IGenericQueryRepository<TDbContext, TEntity>
        where TDbContext : BaseReadOnlyDbContext<TDbContext>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericQueryRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="TDbContext"/>.</param>
        protected GenericQueryRepository(TDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// The FetchAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async Task<IEnumerable<TEntity>> FetchAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        public async Task<TEntity> FetchByIdAsync(long id)
        {
            var entity = await DbSet.FindAsync(id).ConfigureAwait(false);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        public async Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <param name="selector">The selector<see cref="Expression{Func{TEntity, TResult}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TResult}}"/>.</returns>
        public async Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return await DbSet.AsNoTracking().Where(predicate).Select(selector).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The FetchByAndReturnQuerable.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        public IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        /// <summary>
        /// The FetchByCriteriaAsync.
        /// </summary>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IList{TEntity}}"/>.</returns>
        public async Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria)
        {
            var query = DbSet.AsNoTracking()
                .AddPredicate(criteria)
                .AddIncludes(criteria)
                //.AddPaging(criteria)
                .AddSorting(criteria);
            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
