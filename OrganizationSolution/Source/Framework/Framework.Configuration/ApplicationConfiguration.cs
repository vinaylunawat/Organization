namespace Framework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Framework.Constant;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationConfiguration
    {
        private static readonly string _currentEnvironment = Environment.GetEnvironmentVariable(ConfigurationConstant.EnvironmentVariableName);

        public static bool IsDevelopment => IsEnvironment(Environments.Development);

        public static bool IsProduction => IsEnvironment(Environments.Production);

        public static bool IsStaging => IsEnvironment(Environments.Staging);

        public static bool IsEnvironment(string environmentName)
        {
            return environmentName != null && _currentEnvironment?.ToUpperInvariant() == environmentName.ToUpperInvariant();
        }

        public static IHostBuilder DefaultAppConfiguration(this IHostBuilder configurationBuilder, Assembly assembly, string[] args = null)
        {
            return configurationBuilder.DefaultAppConfiguration(new[] { assembly }, args);
        }

        public static IHostBuilder DefaultAppConfiguration(this IHostBuilder configurationBuilder, IEnumerable<Assembly> assemblies, string[] args = null)
        {
            var optionTypes = GetConfigurationOptions(assemblies);
            var options = new List<IConfigurationOptions>();
            return BuilderCreator.Construct(new HostingBuilder(), configurationBuilder, optionTypes, options, args);
        }

        public static IWebHostBuilder DefaultAppConfiguration(this IWebHostBuilder configurationBuilder, Assembly assembly, string[] args = null)
        {
            return configurationBuilder.DefaultAppConfiguration(new[] { assembly }, args);
        }

        public static IWebHostBuilder DefaultAppConfiguration(this IWebHostBuilder configurationBuilder, IEnumerable<Assembly> assemblies, string[] args = null)
        {
            var optionTypes = GetConfigurationOptions(assemblies);
            var options = new List<IConfigurationOptions>();
            return BuilderCreator.Construct(new WebHostingBuilder(), configurationBuilder, optionTypes, options, args);
        }

        private static IEnumerable<Type> GetConfigurationOptions(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(x => x.ExportedTypes
                .Where(t => typeof(IConfigurationOptions).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));
        }
    }
}
