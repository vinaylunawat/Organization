namespace Geography.Storage
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Geography.DataAccess;
    using Framework.Configuration;
    using Framework.Configuration.Models;
    using Geography.Migrations;
    using Framework.Migrations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var builder = new HostBuilder()
                    .DefaultAppConfiguration(typeof(ApplicationOptions).Assembly, args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddDbContext<GeographyDbContext>(
                            (serviceProvider, options) =>
                            {
                                var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();

                                options.UseSqlServer(
                                    applicationOptions.StorageConnectionString,
                                    dbOptions =>
                                    {
                                        dbOptions
                                            .MigrationsAssembly(typeof(MigrationsDbContextFactory).Assembly.GetName().Name)
                                            .MigrationsHistoryTable(GeographyDbContext.DefaultMigrationTableName, GeographyDbContext.DefaultSchemaName);
                                    });
                            }, ServiceLifetime.Scoped);

                        services.AddTransient<IHostedService>((sp) =>
                        {
                            var databaseContext = sp.GetRequiredService<GeographyDbContext>();
                            var applicationOptions = sp.GetRequiredService<ApplicationOptions>();
                            var logger = sp.GetRequiredService<ILogger<DatabaseMigrationService<GeographyDbContext>>>();

                            return new DatabaseMigrationService<GeographyDbContext>(databaseContext, logger, applicationOptions.DeleteDefaultSchema);
                        });
                    });

                using (var host = builder.Build())
                {
                    await host.StartAsync().ConfigureAwait(false);

                    await host.StopAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Console.WriteLine(ex.Message);
                }

                throw;
            }
        }
    }
}
