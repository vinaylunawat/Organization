namespace Geography.DataAccess
{
    using Framework.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="GeographyReadOnlyDbContext" />.
    /// </summary>
    public class GeographyReadOnlyDbContext : BaseReadOnlyDbContext<GeographyReadOnlyDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeographyReadOnlyDbContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{GeographyReadOnlyDbContext}"/>.</param>
        public GeographyReadOnlyDbContext(DbContextOptions<GeographyReadOnlyDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the SchemaName.
        /// </summary>
        public override string SchemaName => GeographyDbContext.DefaultSchemaName;

        /// <summary>
        /// Gets the MigrationTableName.
        /// </summary>
        public override string MigrationTableName => GeographyDbContext.DefaultMigrationTableName;

        /// <summary>
        /// The GetTypeAssemblies.
        /// </summary>
        /// <returns>The <see cref="Assembly[]"/>.</returns>
        public override Assembly[] GetTypeAssemblies()
        {
            return new[] { typeof(GeographyReadOnlyDbContext).Assembly };
        }

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
