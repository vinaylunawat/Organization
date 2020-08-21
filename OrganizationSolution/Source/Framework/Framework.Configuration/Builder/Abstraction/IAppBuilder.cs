namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IAppBuilder{TBuilder}" />.
    /// </summary>
    /// <typeparam name="TBuilder">.</typeparam>
    public interface IAppBuilder<TBuilder>
    {
        /// <summary>
        /// The ConfigureHostConfiguration.
        /// </summary>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        TBuilder ConfigureHostConfiguration(TBuilder builder);

        /// <summary>
        /// The ConfigureAppConfiguration.
        /// </summary>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        TBuilder ConfigureAppConfiguration(TBuilder builder, IEnumerable<Type> types, List<IConfigurationOptions> configurationOptions, string[] args);

        /// <summary>
        /// The ConfigureLogging.
        /// </summary>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <param name="loggingSectionName">The loggingSectionName<see cref="string"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        TBuilder ConfigureLogging(TBuilder builder, string loggingSectionName);

        /// <summary>
        /// The ConfigureServices.
        /// </summary>
        /// <param name="builder">The builder<see cref="TBuilder"/>.</param>
        /// <param name="configurationOptions">The configurationOptions<see cref="List{IConfigurationOptions}"/>.</param>
        /// <returns>The <see cref="TBuilder"/>.</returns>
        TBuilder ConfigureServices(TBuilder builder, List<IConfigurationOptions> configurationOptions);
    }
}
