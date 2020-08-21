namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Criteria;
    using Framework.Service.Extension;
    using Framework.Service.Utilities.Criteria;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="QueryManager{TReadOnlyDbContext, TEntity, TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TReadModel">.</typeparam>
    public abstract class QueryManager<TReadOnlyDbContext, TEntity, TReadModel> : BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel>, IQueryManager<TReadModel>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity, IEntityWithId
        where TReadModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryManager{TReadOnlyDbContext, TEntity, TReadModel}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{QueryManager{TReadOnlyDbContext, TEntity, TReadModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        protected QueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<QueryManager<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="long[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(long id, params long[] ids)
        {
            EnsureArg.IsGt(id, 0, nameof(id));
            return await GetByIdAsync(ids.Prepend(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> ids)
        {
            EnsureArg.IsNotNull(ids, nameof(ids));
            EnsureArgExtensions.HasItems(ids, nameof(ids));
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>
            {
                Predicate = x => ids.Contains(x.Id)
            };
            return await GetByExpressionAsync(filterCriteria).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetAllAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetAllAsync()
        {
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>();
            return await GetByExpressionAsync(filterCriteria).ConfigureAwait(false);
        }

        /// <summary>
        /// The QueryAfterMapAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TReadModel}"/>.</param>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task QueryAfterMapAsync(IEnumerable<TReadModel> models, IEnumerable<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The GetByExpressionAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        protected virtual async Task<IEnumerable<TReadModel>> GetByExpressionAsync(FilterCriteria<TEntity> filterCriteria)
        {
            var result = await GetByQueryExpressionAsync(filterCriteria).ConfigureAwait(false);
            await QueryAfterMapAsync(result.Models, result.Entities).ConfigureAwait(false);
            return result.Models;
        }

        /// <summary>
        /// The FilterAsync.
        /// </summary>
        /// <param name="filterModel">The filterModel<see cref="FilterModel"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> FilterAsync(FilterModel filterModel)
        {
            EnsureArg.IsNotNull(filterModel, nameof(filterModel));

            var filterCriteria = new FilterCriteria<TEntity>()
            {
                Predicate = QuerySearchHelper.Filtering<TEntity>(filterModel.SearchingStrategy.SearchText, filterModel.SearchingStrategy.SearchColumnNames),
                Sort = filterModel.OrderingInfo,
                Paging = filterModel.PagingInfo
            };
            var result = await GetByQueryExpressionForFilterAsync(filterCriteria).ConfigureAwait(false);
            await QueryAfterMapAsync(result.Models, result.Entities).ConfigureAwait(false);
            return result.Models;
        }
    }
}
