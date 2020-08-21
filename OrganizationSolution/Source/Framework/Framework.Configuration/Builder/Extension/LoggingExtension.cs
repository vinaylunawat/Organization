namespace Framework.Configuration
{
    using Framework.Constant;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public static class LoggingExtension
    {
        public static ILoggingBuilder AddLogging(this ILoggingBuilder logging, IConfiguration configuration)
        {
            logging.AddConfiguration(configuration);
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventSourceLogger();
            logging.AddFileLogging(configuration);

            return logging;
        }

        public static ILoggingBuilder AddFileLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration, string defaultLogFilePath = ConfigurationConstant.DefaultLogFilePath, int defaultLogFileSizeInBytes = ConfigurationConstant.DefaultLogFileSizeInBytes)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(
                    defaultLogFilePath,
                    fileSizeLimitBytes: defaultLogFileSizeInBytes,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 15,
                    shared: true,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext:l}.{Method}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            loggingBuilder.AddSerilog(logger);
            return loggingBuilder;
        }
    }
}
