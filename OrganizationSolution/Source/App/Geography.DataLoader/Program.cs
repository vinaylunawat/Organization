namespace Geography.DataLoader
{
    using Geography.Business.Country.Manager;
    using Geography.DataAccess;
    using Framework.Business;
    using Framework.Configuration;
    using Framework.DataLoader;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Framework.Configuration.Models;

    class Program
    {
        public static async Task Main(string[] args)
        {            
            try
            {
                Console.Title = "Geography Dataloader";
                var builder = new HostBuilder()
                    .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly, typeof(DataLoaderOptions).Assembly }, args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddDataUnits();
                        services.AddSingleton<IHostedService, DataLoaderService>();
                        ConfigureDbServices(services);
                        services.AddManagers(typeof(CountryQueryManager).Assembly);
                        services.AddAutoMapper(typeof(CountryQueryManager).Assembly);
                        var serviceProviderBuilt = services.BuildServiceProvider();
                        var applicationOptions = serviceProviderBuilt.GetRequiredService<ApplicationOptions>();

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

                var exception = JsonConvert.SerializeObject(ex);
                Console.WriteLine(exception);
                throw;
            }
        }

        private static void ConfigureDbServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var applicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
            services.ConfigureDbServices(applicationOptions.ConnectionString, applicationOptions.ReadOnlyConnectionString);
        }
    }
}
