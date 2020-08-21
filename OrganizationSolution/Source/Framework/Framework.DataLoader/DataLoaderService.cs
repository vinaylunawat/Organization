namespace Framework.DataLoader
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Framework.Constant;
    using EnsureThat;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public sealed class DataLoaderService : BackgroundService
    {
        public const string ConfigSectionName = ConfigurationConstant.DataLoaderConfigSectionName;

        private readonly IServiceProvider _serviceProvider;
        private readonly DataLoaderOptions _dataLoaderOptions;
        private readonly ILogger<DataLoaderService> _logger;
        private readonly string _serviceName;

        public DataLoaderService(IServiceProvider serviceProvider, ILogger<DataLoaderService> logger, string serviceName = ConfigSectionName)
            : this(serviceProvider, null, logger, serviceName)
        {
        }

        public DataLoaderService(IServiceProvider serviceProvider, DataLoaderOptions dataLoaderOptions, ILogger<DataLoaderService> logger, string serviceName = ConfigSectionName)
        {
            EnsureArg.IsNotNull(serviceProvider, nameof(serviceProvider));
            EnsureArg.IsNotNull(logger, nameof(logger));
            EnsureArg.IsNotNull(serviceName, nameof(serviceName));

            _serviceProvider = serviceProvider;
            _dataLoaderOptions = dataLoaderOptions ?? new DataLoaderOptions();
            _logger = logger;
            _serviceName = serviceName;
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

                _logger.LogInformation($"{_serviceName} runing data units.");

                var dataUnits = _serviceProvider.GetServices<IDataUnit>();

                foreach (var dataUnit in dataUnits)
                {
                    if (!stoppingToken.IsCancellationRequested)
                    {
                        var dataUnitName = dataUnit.GetType().FullName;

                        _logger.LogInformation($"Loading seed data for data unit [{dataUnitName}]...");
                        dataUnit.LoadSeedDataAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        _logger.LogInformation($"Completed loading seed operation for data unit [{dataUnitName}]...");

                        if (_dataLoaderOptions.IncludeDemoData)
                        {
                            _logger.LogInformation($"Loading demo data for data unit [{dataUnitName}]...");
                            dataUnit.LoadDemoDataAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                            _logger.LogInformation($"Completing loading demo data for data unit [{dataUnitName}]...");
                        }
                        else
                        {
                            _logger.LogInformation($"Skipping demo data loading for data unit [{dataUnitName}] since it is turned off in the configuration.");
                        }
                    }
                }

                _logger.LogInformation($"{_serviceName} data units completed successfully.");

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
