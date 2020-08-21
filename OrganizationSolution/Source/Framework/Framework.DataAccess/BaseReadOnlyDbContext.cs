namespace Framework.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="BaseReadOnlyDbContext{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public abstract class BaseReadOnlyDbContext<T> : BaseDbContext<T>
        where T : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReadOnlyDbContext{T}"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{T}"/>.</param>
        public BaseReadOnlyDbContext(DbContextOptions<T> options)
            : base(options)
        {
        }
    }
}
