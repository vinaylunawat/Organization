namespace Framework.Configuration
{
    using System;
    using System.IO;
    using Framework.Constant;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationBuilderExtension
    {
        private const string JsonFileName = ConfigurationConstant.JsonFileName;

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
