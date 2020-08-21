namespace Framework.Business.Manager.Query
{
    using EnsureThat;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Defines the <see cref="BaseQueryManager{TReadOnlyDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class BaseQueryManager<TReadOnlyDbContext, TEntity> : ManagerBase
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryManager{TReadOnlyDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="queryGenericRepository">The queryGenericRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{BaseQueryManager{TReadOnlyDbContext, TEntity}}"/>.</param>
        protected BaseQueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<BaseQueryManager<TReadOnlyDbContext, TEntity>> logger)
            : base(logger)
        {
            EnsureArg.IsNotNull(queryGenericRepository, nameof(queryGenericRepository));
            QueryRepository = queryGenericRepository;
        }

        /// <summary>
        /// Gets the QueryRepository.
        /// </summary>
        protected IGenericQueryRepository<TReadOnlyDbContext, TEntity> QueryRepository { get; private set; }
    }
}
