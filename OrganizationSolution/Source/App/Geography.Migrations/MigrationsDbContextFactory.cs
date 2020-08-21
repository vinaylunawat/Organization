namespace Geography.Migrations
{
    using System;
    using EnsureThat;
    using Geography.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public sealed class MigrationsDbContextFactory : IDesignTimeDbContextFactory<GeographyDbContext>
    {
        private readonly string _connectionString;
        private readonly string _schemaName;
        private readonly string _migrationTableName;

        [Obsolete("This constructor is only to be used by the ef migrations tool", true)]
        public MigrationsDbContextFactory()
            : this("Host=NOTNEEDED;Database=NOTNEEDED;Username=NOTNEEDED;Password=NOTNEEDED")
        {
        }

        private MigrationsDbContextFactory(string connectionString, string schemaName = GeographyDbContext.DefaultSchemaName, string migrationTableName = GeographyDbContext.DefaultMigrationTableName)
        {
            EnsureArg.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            _connectionString = connectionString;
            _schemaName = schemaName;
            _migrationTableName = migrationTableName;
        }

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
