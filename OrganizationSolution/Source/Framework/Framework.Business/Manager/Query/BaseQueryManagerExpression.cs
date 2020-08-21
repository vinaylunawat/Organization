namespace Framework.Business.Manager.Query
{
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel> : BaseQueryManager<TReadOnlyDbContext, TEntity>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity
        where TReadModel : class
    {
        protected BaseQueryManagerExpression(
            IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger)
        {
            EnsureArg.IsNotNull(mapper, nameof(mapper));
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }
        
        protected virtual async Task<IEnumerable<TEntity>> EntityQueryAsync(FilterCriteria<TEntity> filterCriteria)
        {
            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            return await QueryRepository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
        }

        protected virtual async Task<IList<TEntity>> FilterEntityQueryAsync(FilterCriteria<TEntity> filterCriteria)
        {
            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            return await QueryRepository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
        }

#pragma warning disable SA1009
        protected virtual async Task<(IEnumerable<TReadModel> Models, IEnumerable<TEntity> Entities)> GetByQueryExpressionAsync(FilterCriteria<TEntity> filterCriteria)
        {
#pragma warning restore SA1009

            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            var results = new List<TReadModel>();
            var entities = await EntityQueryAsync(filterCriteria).ConfigureAwait(false);
            var models = Mapper.Map(entities, results);
            return new ValueTuple<IEnumerable<TReadModel>, IEnumerable<TEntity>>(models, entities);
        }

        protected virtual async Task<(IEnumerable<TReadModel> Models, IEnumerable<TEntity> Entities)> GetByQueryExpressionForFilterAsync(FilterCriteria<TEntity> filterCriteria)
        {
#pragma warning restore SA1009

            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            var results = new List<TReadModel>();
            var entities = await FilterEntityQueryAsync(filterCriteria).ConfigureAwait(false);
            var models = Mapper.Map(entities, results);
            return new ValueTuple<IEnumerable<TReadModel>, IEnumerable<TEntity>>(models, entities);
        }
    }
}
