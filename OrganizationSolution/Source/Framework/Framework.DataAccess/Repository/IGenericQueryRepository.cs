namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IGenericQueryRepository{TDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IGenericQueryRepository<TDbContext, TEntity>
        where TDbContext : BaseReadOnlyDbContext<TDbContext>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The FetchAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> FetchAllAsync();

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// The FetchByCriteriaAsync.
        /// </summary>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IList{TEntity}}"/>.</returns>
        Task<IList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria);

        /// <summary>
        /// The FetchByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> FetchByIdAsync(long id);

        /// <summary>
        /// The FetchByAsync.
        /// </summary>
        /// <typeparam name="TResult">.</typeparam>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <param name="selector">The selector<see cref="Expression{Func{TEntity, TResult}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TResult}}"/>.</returns>
        Task<IEnumerable<TResult>> FetchByAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// The FetchByAndReturnQuerable.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        IQueryable<TEntity> FetchByAndReturnQuerable(Expression<Func<TEntity, bool>> predicate);
    }
}
