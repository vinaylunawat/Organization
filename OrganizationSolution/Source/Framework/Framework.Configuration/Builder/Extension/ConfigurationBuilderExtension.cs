namespace Framework.Configuration
{
    using Framework.Constant;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ConfigurationBuilderExtension" />.
    /// </summary>
    public static class ConfigurationBuilderExtension
    {
        /// <summary>
        /// Defines the JsonFileName.
        /// </summary>
        private const string JsonFileName = ConfigurationConstant.JsonFileName;

        /// <summary>
        /// The AddAppConfiguration.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfigurationBuilder"/>.</param>
        /// <param name="environmentName">The environmentName<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder configuration, string environmentName, string[] args)
        {
            string fileName = $"appsettings.{environmentName}.json";

            configuration.AddJsonFile(JsonFileName, false, true)
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName), true, true)
                .AddJsonFile(fileName, true, true);

            if (args != null)
            {
                configuration.AddCommandLine(args);
            }

            return configuration;
        }
    }
}
