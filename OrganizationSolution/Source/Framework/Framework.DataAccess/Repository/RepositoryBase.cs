namespace Framework.DataAccess.Repository
{
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<TDbContext, TEntity> : IRepositoryBase
        where TDbContext : DbContext
        where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;
        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>() as DbSet<TEntity>;
    }
}
