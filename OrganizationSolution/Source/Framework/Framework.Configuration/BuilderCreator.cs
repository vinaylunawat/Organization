namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="BuilderCreator" />.
    /// </summary>
    public static class BuilderCreator
    {
        /// <summary>
        /// The Construct.
        /// </summary>
        /// <typeparam name="TBuilder">.</typeparam>
        /// <param name="appBuilder">The appBuilder<see cref="IAppBuilder{TBuilder}"/>.</param>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <param name="optionTypes">The optionTypes<see cref="IEnumerable{Type}"/>.</param>
        /// <param name="options">The options<see cref="List{IConfigurationOptions}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <param name="loggingSectionName">The loggingSectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
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
