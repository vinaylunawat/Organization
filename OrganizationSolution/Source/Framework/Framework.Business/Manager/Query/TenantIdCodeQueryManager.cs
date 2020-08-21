namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using DataAccess;
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.Business.Models;
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
    /// Defines the <see cref="TenantIdCodeQueryManager{TReadOnlyDbContext, TEntity, TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TReadModel">.</typeparam>
    public abstract class TenantIdCodeQueryManager<TReadOnlyDbContext, TEntity, TReadModel> : TenantIdQueryManager<TReadOnlyDbContext, TEntity, TReadModel>, ITenantIdCodeQueryManager<TReadModel>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity, IEntityWithId, IEntityWithCode, IEntityWithTenantId
        where TReadModel : class, IModelWithCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdCodeQueryManager{TReadOnlyDbContext, TEntity, TReadModel}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{TenantIdCodeQueryManager{TReadOnlyDbContext, TEntity, TReadModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        protected TenantIdCodeQueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<TenantIdCodeQueryManager<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        /// <summary>
        /// The GetByCodeAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="codes">The codes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetByCodeAsync(IEnumerable<long> tenantIds, string code, params string[] codes)
        {
            EnsureArg.IsNotNull(code, nameof(code));
            return await GetByCodeAsync(tenantIds, codes.Prepend(code)).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetByCodeAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="codes">The codes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        public virtual async Task<IEnumerable<TReadModel>> GetByCodeAsync(IEnumerable<long> tenantIds, IEnumerable<string> codes)
        {
            EnsureArg.IsNotNull(tenantIds, nameof(tenantIds));
            EnsureArgExtensions.HasItems(tenantIds, nameof(tenantIds));

            EnsureArg.IsNotNull(codes, nameof(codes));
            EnsureArgExtensions.HasItems(codes, nameof(codes));
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>
            {
                Predicate = x => codes.Contains(x.Code) && tenantIds.Contains(x.TenantId)
            };
            return await GetByExpressionAsync(tenantIds, filterCriteria).ConfigureAwait(false);
        }
    }
}
