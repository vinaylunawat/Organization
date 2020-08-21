namespace Geography.Migrations
{
    using EnsureThat;
    using Geography.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using System;

    /// <summary>
    /// Defines the <see cref="MigrationsDbContextFactory" />.
    /// </summary>
    public sealed class MigrationsDbContextFactory : IDesignTimeDbContextFactory<GeographyDbContext>
    {
        /// <summary>
        /// Defines the _connectionString.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Defines the _schemaName.
        /// </summary>
        private readonly string _schemaName;

        /// <summary>
        /// Defines the _migrationTableName.
        /// </summary>
        private readonly string _migrationTableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationsDbContextFactory"/> class.
        /// </summary>
        [Obsolete("This constructor is only to be used by the ef migrations tool", true)]
        public MigrationsDbContextFactory()
            : this("Host=NOTNEEDED;Database=NOTNEEDED;Username=NOTNEEDED;Password=NOTNEEDED")
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MigrationsDbContextFactory"/> class from being created.
        /// </summary>
        /// <param name="connectionString">The connectionString<see cref="string"/>.</param>
        /// <param name="schemaName">The schemaName<see cref="string"/>.</param>
        /// <param name="migrationTableName">The migrationTableName<see cref="string"/>.</param>
        private MigrationsDbContextFactory(string connectionString, string schemaName = GeographyDbContext.DefaultSchemaName, string migrationTableName = GeographyDbContext.DefaultMigrationTableName)
        {
            EnsureArg.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            _connectionString = connectionString;
            _schemaName = schemaName;
            _migrationTableName = migrationTableName;
        }

        /// <summary>
        /// The CreateDbContext.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="GeographyDbContext"/>.</returns>
        [Obsolete("This method is only to be used by the ef migrations tool", true)]
        public GeographyDbContext CreateDbContext(params string[] args)
        {
            var databaseContextOptionsBuilder = new DbContextOptionsBuilder<GeographyDbContext>()
                .UseSqlServer(
                    _connectionString,
                    options =>
                    {
                        options.MigrationsAssembly(typeof(MigrationsDbContextFactory).Assembly.GetName().Name).MigrationsHistoryTable(_migrationTableName, _schemaName);
                    });

            return new GeographyDbContext(databaseContextOptionsBuilder.Options);
        }
    }
}
