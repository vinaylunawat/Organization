namespace Framework.DataLoader
{
    using EnsureThat;
    using Framework.Constant;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DataLoaderService" />.
    /// </summary>
    public sealed class DataLoaderService : BackgroundService
    {
        /// <summary>
        /// Defines the ConfigSectionName.
        /// </summary>
        public const string ConfigSectionName = ConfigurationConstant.DataLoaderConfigSectionName;

        /// <summary>
        /// Defines the _serviceProvider.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Defines the _dataLoaderOptions.
        /// </summary>
        private readonly DataLoaderOptions _dataLoaderOptions;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<DataLoaderService> _logger;

        /// <summary>
        /// Defines the _serviceName.
        /// </summary>
        private readonly string _serviceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataLoaderService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{DataLoaderService}"/>.</param>
        /// <param name="serviceName">The serviceName<see cref="string"/>.</param>
        public DataLoaderService(IServiceProvider serviceProvider, ILogger<DataLoaderService> logger, string serviceName = ConfigSectionName)
            : this(serviceProvider, null, logger, serviceName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataLoaderService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/>.</param>
        /// <param name="dataLoaderOptions">The dataLoaderOptions<see cref="DataLoaderOptions"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{DataLoaderService}"/>.</param>
        /// <param name="serviceName">The serviceName<see cref="string"/>.</param>
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
