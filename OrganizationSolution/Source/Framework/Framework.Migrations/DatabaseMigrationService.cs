namespace Framework.Migrations
{
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DatabaseMigrationService{TDbContext}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    public sealed class DatabaseMigrationService<TDbContext> : BackgroundService
        where TDbContext : DbContext
    {
        /// <summary>
        /// Defines the _databaseContext.
        /// </summary>
        private readonly TDbContext _databaseContext;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<DatabaseMigrationService<TDbContext>> _logger;

        /// <summary>
        /// Defines the _deleteDefaultSchema.
        /// </summary>
        private readonly bool _deleteDefaultSchema;

        /// <summary>
        /// Defines the _deleteDatabase.
        /// </summary>
        private readonly bool _deleteDatabase;

        /// <summary>
        /// Defines the _serviceName.
        /// </summary>
        private readonly string _serviceName;

        /// <summary>
        /// Defines the _targetMigration.
        /// </summary>
        private readonly string _targetMigration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseMigrationService{TDbContext}"/> class.
        /// </summary>
        /// <param name="databaseContext">The databaseContext<see cref="TDbContext"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{DatabaseMigrationService{TDbContext}}"/>.</param>
        /// <param name="deleteDefaultSchema">The deleteDefaultSchema<see cref="bool"/>.</param>
        /// <param name="deleteDatabase">The deleteDatabase<see cref="bool"/>.</param>
        /// <param name="serviceName">The serviceName<see cref="string"/>.</param>
        /// <param name="targetMigration">The targetMigration<see cref="string"/>.</param>
        public DatabaseMigrationService(TDbContext databaseContext, ILogger<DatabaseMigrationService<TDbContext>> logger, bool deleteDefaultSchema = false, bool deleteDatabase = false, string serviceName = "Database Migration", string targetMigration = null)
        {
            EnsureArg.IsNotNull(databaseContext, nameof(databaseContext));
            EnsureArg.IsNotNull(logger, nameof(logger));
            EnsureArg.IsNotNull(serviceName, nameof(serviceName));

            _databaseContext = databaseContext;
            _logger = logger;
            _deleteDefaultSchema = deleteDefaultSchema;
            _deleteDatabase = deleteDatabase;
            _serviceName = serviceName;
            _targetMigration = targetMigration;
        }

        /// <summary>
        /// The ExecuteAsync.
        /// </summary>
        /// <param name="stoppingToken">The stoppingToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogDebug($"{_serviceName} background task starting.");

                stoppingToken.Register(() =>
                {
                    _logger.LogDebug($"{_serviceName} background task is cancelling.");
                });

                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Connected to: {_databaseContext.Database.GetDbConnection().ConnectionString}");

                    _databaseContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                    if (_deleteDatabase)
                    {
                        _logger.LogInformation("Deleting database...");
                        if (!_databaseContext.Database.EnsureDeleted())
                        {
                            _logger.LogInformation("No database found to delete.");
                        }
                        else
                        {
                            _logger.LogInformation("Deleting database successful.");
                        }
                    }
                    else if (_deleteDefaultSchema && _databaseContext.Database.CanConnect())
                    {
                        _logger.LogInformation("Deleting schema...");
                        var defaultSchema = _databaseContext.Model.GetDefaultSchema();
                        _logger.LogInformation("Deleting schema successful.");
                    }

                    _logger.LogInformation("Starting database migration...");

                    if (string.IsNullOrWhiteSpace(_targetMigration))
                    {
                        _databaseContext.Database.Migrate();
                    }
                    else
                    {
                        _logger.LogDebug($"Migrating to specific migration: {_targetMigration}");

                        var migrator = _databaseContext.GetService<IMigrator>();
                        await migrator.MigrateAsync(_targetMigration).ConfigureAwait(false);
                    }

                    _logger.LogInformation("Database migration completed successfully.");
                }

                await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);

                _logger.LogDebug($"{_serviceName} background task execution completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An unhandled exception occured in {_serviceName} please check the logs.");
                throw;
            }
        }
    }
}
