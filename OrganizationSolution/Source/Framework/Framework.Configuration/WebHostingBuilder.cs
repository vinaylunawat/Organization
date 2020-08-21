namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;
    using Framework.Constant;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class WebHostingBuilder : AbstractAppBuilder, IAppBuilder<IWebHostBuilder>
    {
        public IWebHostBuilder ConfigureHostConfiguration(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder;
        }

        public IWebHostBuilder ConfigureAppConfiguration(IWebHostBuilder webHostBuilder, IEnumerable<Type> types, List<IConfigurationOptions> configurationOptions, string[] args)
        {
            return webHostBuilder.ConfigureAppConfiguration((webHostBuilderContext, builderConfiguration) =>
            {
                ConfigureApp(webHostBuilderContext.HostingEnvironment.EnvironmentName, builderConfiguration, types, configurationOptions, args);
            });
        }

        public IWebHostBuilder ConfigureLogging(IWebHostBuilder webHostBuilder, string loggingSectionName = ConfigurationConstant.loggingSectionName)
        {
            return webHostBuilder.ConfigureLogging((webHostBuilderContext, logging) =>
            {
                var configuration = webHostBuilderContext.Configuration.GetSection(loggingSectionName);
                logging.AddLogging(configuration);
            });
        }

        public IWebHostBuilder ConfigureServices(IWebHostBuilder webHostBuilder, List<IConfigurationOptions> configurationOptions)
        {
            return webHostBuilder.ConfigureServices((hostingContext, services) =>
            {
                configurationOptions.ForEach(x => services.AddSingleton(x.GetType(), x));
            });
        }
    }
}
