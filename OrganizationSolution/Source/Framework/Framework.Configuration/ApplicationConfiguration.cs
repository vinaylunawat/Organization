namespace Framework.Configuration
{
    using Framework.Constant;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="ApplicationConfiguration" />.
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Defines the _currentEnvironment.
        /// </summary>
        private static readonly string _currentEnvironment = Environment.GetEnvironmentVariable(ConfigurationConstant.EnvironmentVariableName);

        /// <summary>
        /// Gets a value indicating whether IsDevelopment.
        /// </summary>
        public static bool IsDevelopment => IsEnvironment(Environments.Development);

        /// <summary>
        /// Gets a value indicating whether IsProduction.
        /// </summary>
        public static bool IsProduction => IsEnvironment(Environments.Production);

        /// <summary>
        /// Gets a value indicating whether IsStaging.
        /// </summary>
        public static bool IsStaging => IsEnvironment(Environments.Staging);

        /// <summary>
        /// The IsEnvironment.
        /// </summary>
        /// <param name="environmentName">The environmentName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEnvironment(string environmentName)
        {
            return environmentName != null && _currentEnvironment?.ToUpperInvariant() == environmentName.ToUpperInvariant();
        }

        /// <summary>
        /// The DefaultAppConfiguration.
        /// </summary>
        /// <param name="configurationBuilder">The configurationBuilder<see cref="IHostBuilder"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder DefaultAppConfiguration(this IHostBuilder configurationBuilder, Assembly assembly, string[] args = null)
        {
            return configurationBuilder.DefaultAppConfiguration(new[] { assembly }, args);
        }

        /// <summary>
        /// The DefaultAppConfiguration.
        /// </summary>
        /// <param name="configurationBuilder">The configurationBuilder<see cref="IHostBuilder"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder DefaultAppConfiguration(this IHostBuilder configurationBuilder, IEnumerable<Assembly> assemblies, string[] args = null)
        {
            var optionTypes = GetConfigurationOptions(assemblies);
            var options = new List<IConfigurationOptions>();
            return BuilderCreator.Construct(new HostingBuilder(), configurationBuilder, optionTypes, options, args);
        }

        /// <summary>
        /// The DefaultAppConfiguration.
        /// </summary>
        /// <param name="configurationBuilder">The configurationBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <param name="assembly">The assembly<see cref="Assembly"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder DefaultAppConfiguration(this IWebHostBuilder configurationBuilder, Assembly assembly, string[] args = null)
        {
            return configurationBuilder.DefaultAppConfiguration(new[] { assembly }, args);
        }

        /// <summary>
        /// The DefaultAppConfiguration.
        /// </summary>
        /// <param name="configurationBuilder">The configurationBuilder<see cref="IWebHostBuilder"/>.</param>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder DefaultAppConfiguration(this IWebHostBuilder configurationBuilder, IEnumerable<Assembly> assemblies, string[] args = null)
        {
            var optionTypes = GetConfigurationOptions(assemblies);
            var options = new List<IConfigurationOptions>();
            return BuilderCreator.Construct(new WebHostingBuilder(), configurationBuilder, optionTypes, options, args);
        }

        /// <summary>
        /// The GetConfigurationOptions.
        /// </summary>
        /// <param name="assemblies">The assemblies<see cref="IEnumerable{Assembly}"/>.</param>
        /// <returns>The <see cref="IEnumerable{Type}"/>.</returns>
        private static IEnumerable<Type> GetConfigurationOptions(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IConfigurationOptions).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));
        }
    }
}
