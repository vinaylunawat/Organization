namespace Framework.Business.Manager.Query
{
    using AutoMapper;
    using EnsureThat;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="BaseQueryManagerExpression{TReadOnlyDbContext, TEntity, TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TReadModel">.</typeparam>
    public class BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel> : BaseQueryManager<TReadOnlyDbContext, TEntity>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity
        where TReadModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryManagerExpression{TReadOnlyDbContext, TEntity, TReadModel}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{BaseQueryManagerExpression{TReadOnlyDbContext, TEntity, TReadModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        protected BaseQueryManagerExpression(
            IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<BaseQueryManagerExpression<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger)
        {
            EnsureArg.IsNotNull(mapper, nameof(mapper));
            Mapper = mapper;
        }

        /// <summary>
        /// Gets the Mapper.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// The EntityQueryAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        protected virtual async Task<IEnumerable<TEntity>> EntityQueryAsync(FilterCriteria<TEntity> filterCriteria)
        {
            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            return await QueryRepository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
        }

        /// <summary>
        /// The FilterEntityQueryAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IList{TEntity}}"/>.</returns>
        protected virtual async Task<IList<TEntity>> FilterEntityQueryAsync(FilterCriteria<TEntity> filterCriteria)
        {
            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            return await QueryRepository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
        }

#pragma warning disable SA1009
        /// <summary>
        /// The GetByQueryExpressionAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{(IEnumerable{TReadModel} Models, IEnumerable{TEntity} Entities)}"/>.</returns>
        protected virtual async Task<(IEnumerable<TReadModel> Models, IEnumerable<TEntity> Entities)> GetByQueryExpressionAsync(FilterCriteria<TEntity> filterCriteria)
        {
#pragma warning restore SA1009

            EnsureArg.IsNotNull(filterCriteria, nameof(filterCriteria));
            var results = new List<TReadModel>();
            var entities = await EntityQueryAsync(filterCriteria).ConfigureAwait(false);
            var models = Mapper.Map(entities, results);
            return new ValueTuple<IEnumerable<TReadModel>, IEnumerable<TEntity>>(models, entities);
        }

        /// <summary>
        /// The GetByQueryExpressionForFilterAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{(IEnumerable{TReadModel} Models, IEnumerable{TEntity} Entities)}"/>.</returns>
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
