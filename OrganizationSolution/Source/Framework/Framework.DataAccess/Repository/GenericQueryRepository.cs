namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Framework.Service.Extension;
    using Framework.Service.Utilities.Criteria;
    using Framework.Service.Paging.Abstraction;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class GenericQueryRepository<TDbContext, TEntity> : RepositoryBase<TDbContext, TEntity>, IGenericQueryRepository<TDbContext, TEntity>
        where TDbContext : BaseReadOnlyDbContext<TDbContext>
        where TEntity : BaseEntity
    {
        private readonly TDbContext _dbContext;

        protected GenericQueryRepository(TDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> FetchAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> FetchByIdAsync(long id)
        {
            var entity = await DbSet.FindAsync(id).ConfigureAwait(false);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return await DbSet.AsNoTracking().Where(predicate).Select(selector).ToListAsync().ConfigureAwait(false);
        }

        public IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public async Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria)
        {
            var query = DbSet.AsNoTracking()
                .AddPredicate(criteria)
                .AddIncludes(criteria)
                //.AddPaging(criteria)
                .AddSorting(criteria);
            return await query.ToListAsync().ConfigureAwait(false);
            //Todo
            //return await query.ToPaginatedListAsync(criteria?.Paging?.PageNumber ?? 0, criteria?.Paging?.PageSize ?? 0).ConfigureAwait(false);
        }

    }
}
