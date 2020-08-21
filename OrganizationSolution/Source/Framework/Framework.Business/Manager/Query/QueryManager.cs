namespace Framework.Business.Manager.Query
{
    using Framework.Business.Extension;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Criteria;
    using Framework.Service.Extension;
    using Framework.Service.Utilities.Criteria;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class QueryManager<TReadOnlyDbContext, TEntity, TReadModel> : BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel>, IQueryManager<TReadModel>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity, IEntityWithId
        where TReadModel : class
    {
        protected QueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<QueryManager<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(long id, params long[] ids)
        {
            EnsureArg.IsGt(id, 0, nameof(id));
            return await GetByIdAsync(ids.Prepend(id)).ConfigureAwait(false);
        }

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

        public virtual async Task<IEnumerable<TReadModel>> GetAllAsync()
        {
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>();            
            return await GetByExpressionAsync(filterCriteria).ConfigureAwait(false);            
        }

        protected virtual Task QueryAfterMapAsync(IEnumerable<TReadModel> models, IEnumerable<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        protected virtual async Task<IEnumerable<TReadModel>> GetByExpressionAsync(FilterCriteria<TEntity> filterCriteria)
        {
            var result = await GetByQueryExpressionAsync(filterCriteria).ConfigureAwait(false);
            await QueryAfterMapAsync(result.Models, result.Entities).ConfigureAwait(false);
            return result.Models;
        }

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
