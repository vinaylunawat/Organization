namespace Framework.Business.Manager.Query
{
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    public abstract class BaseQueryManager<TReadOnlyDbContext, TEntity> : ManagerBase
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity
    {
        protected BaseQueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<BaseQueryManager<TReadOnlyDbContext, TEntity>> logger)
            : base(logger)
        {
            EnsureArg.IsNotNull(queryGenericRepository, nameof(queryGenericRepository));
            QueryRepository = queryGenericRepository;
        }

        protected IGenericQueryRepository<TReadOnlyDbContext, TEntity> QueryRepository { get; private set; }
    }
}
