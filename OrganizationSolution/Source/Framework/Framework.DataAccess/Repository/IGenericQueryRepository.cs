namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using Framework.Service.Paging.Abstraction;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericQueryRepository<TDbContext, TEntity>
        where TDbContext : BaseReadOnlyDbContext<TDbContext>
        where TEntity : BaseEntity
    {

        Task<IEnumerable<TEntity>> FetchAllAsync();

        Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria);

        Task<TEntity> FetchByIdAsync(long id);

        Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

        IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate);
    }
}
