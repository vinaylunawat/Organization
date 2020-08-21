namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using DataAccess;
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TenantIdQueryManager{TReadOnlyDbContext, TEntity, TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TReadModel">.</typeparam>
    public abstract class TenantIdQueryManager<TReadOnlyDbContext, TEntity, TReadModel> : BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel>, ITenantIdQueryManager<TReadModel>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity, IEntityWithId, IEntityWithTenantId
        where TReadModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdQueryManager{TReadOnlyDbContext, TEntity, TReadModel}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{TenantIdQueryManager{TReadOnlyDbContext, TEntity, TReadModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        protected TenantIdQueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<TenantIdQueryManager<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="long[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public async Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> tenantIds, long id, params long[] ids)
        {
            EnsureArg.IsGt(id, 0, nameof(id));
            return await GetByIdAsync(tenantIds, ids.Prepend(id)).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> tenantIds, IEnumerable<long> ids)
        {
            EnsureArg.IsNotNull(tenantIds, nameof(tenantIds));
            EnsureArgExtensions.HasItems(tenantIds, nameof(tenantIds));

            EnsureArg.IsNotNull(ids, nameof(ids));
            EnsureArgExtensions.HasItems(ids, nameof(ids));
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>
            {
                Predicate = x => ids.Contains(x.Id) && tenantIds.Contains(x.TenantId)
            };
            return await GetByExpressionAsync(tenantIds, filterCriteria).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetAllAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetAllAsync(IEnumerable<long> tenantIds)
        {
            EnsureArg.IsNotNull(tenantIds, nameof(tenantIds));
            EnsureArgExtensions.HasItems(tenantIds, nameof(tenantIds));
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>
            {
                Predicate = x => tenantIds.Contains(x.TenantId)
            };
            return await GetByExpressionAsync(tenantIds, filterCriteria).ConfigureAwait(false);
        }

        /// <summary>
        /// The QueryAfterMapAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="models">The models<see cref="IList{TReadModel}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task QueryAfterMapAsync(IEnumerable<long> tenantIds, IList<TReadModel> models, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The GetByExpressionAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        protected virtual async Task<IEnumerable<TReadModel>> GetByExpressionAsync(IEnumerable<long> tenantIds, FilterCriteria<TEntity> filterCriteria)
        {
            var result = await GetByQueryExpressionAsync(filterCriteria).ConfigureAwait(false);
            await QueryAfterMapAsync(tenantIds, result.Models.ToList(), result.Entities.ToList()).ConfigureAwait(false);
            return result.Models;
        }
    }
}
