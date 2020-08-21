namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;
    public static class BuilderCreator
    {
        public static TBuilder Construct<TBuilder>(IAppBuilder<TBuilder> appBuilder, TBuilder builder, IEnumerable<Type> optionTypes, List<IConfigurationOptions> options, string[] args = null, string loggingSectionName = "Logging")
        {
            builder = appBuilder.ConfigureHostConfiguration(builder);
            builder = appBuilder.ConfigureAppConfiguration(builder, optionTypes, options, args);
            builder = appBuilder.ConfigureLogging(builder, loggingSectionName);
            builder = appBuilder.ConfigureServices(builder, options);
            return builder;
        }
    }
}
