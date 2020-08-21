namespace Framework.Migrations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public sealed class DatabaseMigrationService<TDbContext> : BackgroundService
        where TDbContext : DbContext
    {
        private readonly TDbContext _databaseContext;
        private readonly ILogger<DatabaseMigrationService<TDbContext>> _logger;
        private readonly bool _deleteDefaultSchema;
        private readonly bool _deleteDatabase;
        private readonly string _serviceName;
        private readonly string _targetMigration;

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
