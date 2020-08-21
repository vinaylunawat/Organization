namespace Framework.DataAccess.Repository
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="RepositoryBase{TDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class RepositoryBase<TDbContext, TEntity> : IRepositoryBase
        where TDbContext : DbContext
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Defines the _dbContext.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="DbContext"/>.</param>
        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the DbSet.
        /// </summary>
        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>() as DbSet<TEntity>;
    }
}
