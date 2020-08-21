namespace Framework.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseReadOnlyDbContext<T> : BaseDbContext<T>
        where T : DbContext
    {
        public BaseReadOnlyDbContext(DbContextOptions<T> options)
            : base(options)
        {
            // todo: set no tracking, no proxy etc on the context
        }
    }
}
