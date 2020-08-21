namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;

    public interface IAppBuilder<TBuilder>
    {
        TBuilder ConfigureHostConfiguration(TBuilder builder);

        TBuilder ConfigureAppConfiguration(TBuilder builder, IEnumerable<Type> types, List<IConfigurationOptions> configurationOptions, string[] args);

        TBuilder ConfigureLogging(TBuilder builder, string loggingSectionName);

        TBuilder ConfigureServices(TBuilder builder, List<IConfigurationOptions> configurationOptions);
    }
}
