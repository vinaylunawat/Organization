namespace Framework.Configuration
{
    using Framework.Constant;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WebHostingBuilder" />.
    /// </summary>
    public class WebHostingBuilder : AbstractAppBuilder, IAppBuilder<IWebHostBuilder>
    {
        /// <summary>
        /// The ConfigureHostConfiguration.
        /// </summary>
        /// <param name="webHostBuilder">The webHostBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureHostConfiguration(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder;
        }

        /// <summary>
        /// The ConfigureAppConfiguration.
        /// </summary>
        /// <param name="webHostBuilder">The webHostBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureAppConfiguration(IWebHostBuilder webHostBuilder, IEnumerable<Type> types, List<IConfigurationOptions> configurationOptions, string[] args)
        {
            return webHostBuilder.ConfigureAppConfiguration((webHostBuilderContext, builderConfiguration) =>
            {
                ConfigureApp(webHostBuilderContext.HostingEnvironment.EnvironmentName, builderConfiguration, types, configurationOptions, args);
            });
        }

        /// <summary>
        /// The ConfigureLogging.
        /// </summary>
        /// <param name="webHostBuilder">The webHostBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <param name="loggingSectionName">The loggingSectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureLogging(IWebHostBuilder webHostBuilder, string loggingSectionName = ConfigurationConstant.loggingSectionName)
        {
            return webHostBuilder.ConfigureLogging((webHostBuilderContext, logging) =>
            {
                var configuration = webHostBuilderContext.Configuration.GetSection(loggingSectionName);
                logging.AddLogging(configuration);
            });
        }

        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="webHostBuilder">The webHostBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public IWebHostBuilder ConfigureServices(IWebHostBuilder webHostBuilder, List<IConfigurationOptions> configurationOptions)
        {
            return webHostBuilder.ConfigureServices((hostingContext, services) =>
            {
                configurationOptions.ForEach(x => services.AddSingleton(x.GetType(), x));
            });
        }
    }
}
