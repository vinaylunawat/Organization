namespace Geography.DataAccess
{
    using Framework.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="GeographyDbContext" />.
    /// </summary>
    public class GeographyDbContext : BaseDbContext<GeographyDbContext>
    {
        /// <summary>
        /// Defines the DefaultSchemaName.
        /// </summary>
        public const string DefaultSchemaName = "Geography";

        /// <summary>
        /// Defines the DefaultMigrationTableName.
        /// </summary>
        public const string DefaultMigrationTableName = "Geography_migrations";

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographyDbContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The dbContextOptions<see cref="DbContextOptions{GeographyDbContext}"/>.</param>
        public GeographyDbContext(DbContextOptions<GeographyDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        /// <summary>
        /// Gets the SchemaName.
        /// </summary>
        public override string SchemaName
        {
            get { return DefaultSchemaName; }
        }

        /// <summary>
        /// Gets the MigrationTableName.
        /// </summary>
        public override string MigrationTableName
        {
            get { return DefaultMigrationTableName; }
        }

        /// <summary>
        /// The GetTypeAssemblies.
        /// </summary>
        /// <returns>The <see cref="Assembly[]"/>.</returns>
        public override Assembly[] GetTypeAssemblies()
        {
            return new[] { typeof(GeographyDbContext).Assembly };
        }
    }
}
