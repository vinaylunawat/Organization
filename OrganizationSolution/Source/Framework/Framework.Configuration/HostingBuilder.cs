namespace Framework.Configuration
{
    using Framework.Constant;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="HostingBuilder" />.
    /// </summary>
    public class HostingBuilder : AbstractAppBuilder, IAppBuilder<IHostBuilder>
    {
        /// <summary>
        /// Defines the EnvironmentVariablePrefix.
        /// </summary>
        private const string EnvironmentVariablePrefix = ConfigurationConstant.EnvironmentVariablePrefix;

        /// <summary>
        /// The ConfigureHostConfiguration.
        /// </summary>
        /// <param name="hostBuilder">The hostBuilder<see cref="IHostBuilder"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public IHostBuilder ConfigureHostConfiguration(IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureHostConfiguration(configuration =>
            {
                configuration.AddEnvironmentVariables(prefix: EnvironmentVariablePrefix);
            });
        }

        /// <summary>
        /// The ConfigureAppConfiguration.
        /// </summary>
        /// <param name="hostBuilder">The hostBuilder<see cref="IHostBuilder"/>.</param>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/>.</param>
        /// <param name="options">The options<see cref="List{IConfigurationOptions}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public IHostBuilder ConfigureAppConfiguration(IHostBuilder hostBuilder, IEnumerable<Type> types, List<IConfigurationOptions> options, string[] args)
        {
            return hostBuilder.ConfigureAppConfiguration((webHostBuilderContext, builderConfiguration) =>
            {
                ConfigureApp(webHostBuilderContext.HostingEnvironment.EnvironmentName, builderConfiguration, types, options, args);
            });
        }

        /// <summary>
        /// The ConfigureLogging.
        /// </summary>
        /// <param name="hostBuilder">The hostBuilder<see cref="IHostBuilder"/>.</param>
        /// <param name="loggingSectionName">The loggingSectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public IHostBuilder ConfigureLogging(IHostBuilder hostBuilder, string loggingSectionName)
        {
            return hostBuilder.ConfigureLogging((webHostBuilderContext, logging) =>
            {
                var configuration = webHostBuilderContext.Configuration.GetSection(loggingSectionName);
                logging.AddLogging(configuration);
            });
        }

        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="hostBuilder">The hostBuilder<see cref="IHostBuilder"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public IHostBuilder ConfigureServices(IHostBuilder hostBuilder, List<IConfigurationOptions> configurationOptions)
        {
            return hostBuilder.ConfigureServices((hostingContext, services) =>
            {
                configurationOptions.ForEach(x => services.AddSingleton(x.GetType(), x));
            });
        }
    }
}
