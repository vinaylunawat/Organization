namespace Framework.Business.Extension
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="LoggerExtensions" />.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// The LogTrace.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogTrace<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Trace, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogDebug.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogDebug<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Debug, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogInformation.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogInformation<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Information, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogWarning.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogWarning<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Warning, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogError.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogError<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Error, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogCritical.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public static void LogCritical<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Critical, managerResponse, actionName, message);
        }

        /// <summary>
        /// The LogTrace.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogTrace<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Trace, apiException, message, args);
        }

        /// <summary>
        /// The LogDebug.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogDebug<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Debug, apiException, message, args);
        }

        /// <summary>
        /// The LogInformation.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogInformation<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Information, apiException, message, args);
        }

        /// <summary>
        /// The LogWarning.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogWarning<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Warning, apiException, message, args);
        }

        /// <summary>
        /// The LogError.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogError<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Error, apiException, message, args);
        }

        /// <summary>
        /// The LogCritical.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        public static void LogCritical<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Critical, apiException, message, args);
        }

        /// <summary>
        /// The LogApiExceptionHelper.
        /// </summary>
        /// <typeparam name="TExceptionType">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="level">The level<see cref="LogLevel"/>.</param>
        /// <param name="apiException">The apiException<see cref="TExceptionType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="args">The args<see cref="object[]"/>.</param>
        private static void LogApiExceptionHelper<TExceptionType>(ILogger logger, LogLevel level, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine(message);

                // hack for the Generic ApiException<>
                var resultProperty = apiException.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    sb.Append("Result: ");
                    var result = resultProperty.GetValue(apiException);
                    if (result != null)
                    {
                        sb.Append(JsonConvert.SerializeObject(result));
                    }
                }

                sb.Append("Headers: ");
                foreach (var header in apiException.Headers)
                {
                    sb.Append($"{header.Key} = {string.Join(",", header.Value.ToArray())}");
                }

                logger.Log(level, apiException, $"An API Exception occured. StatusCode: {apiException.StatusCode} Response: {apiException.Response} {sb.ToString()}", args);
            }
            finally
            {
            }
        }

        /// <summary>
        /// The LogManagerResponseHelper.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="logger">The logger<see cref="ILogger"/>.</param>
        /// <param name="level">The level<see cref="LogLevel"/>.</param>
        /// <param name="managerResponse">The managerResponse<see cref="ManagerResponse{TErrorCode}"/>.</param>
        /// <param name="actionName">The actionName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        private static void LogManagerResponseHelper<TErrorCode>(ILogger logger, LogLevel level, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            try
            {
                var sb = new StringBuilder();
                if (managerResponse.HasError)
                {
                    if (!string.IsNullOrWhiteSpace(actionName))
                    {
                        sb.AppendLine($"Action [{actionName}] encountered errors.");
                    }

                    foreach (var errorRecord in managerResponse.ErrorRecords)
                    {
                        foreach (var errorMessage in errorRecord.Errors)
                        {
                            sb.AppendLine($"Ordinal Position: {errorRecord.OrdinalPosition} | Id: {errorRecord.Id} | Error Code: {errorMessage.ErrorCode} | Property Name: {errorMessage.PropertyName} | Attempted Value: {errorMessage.AttemptedValue} | Message: {errorMessage.Message}");
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(actionName))
                    {
                        sb.AppendLine($"Action [{actionName}] completed without error.");
                    }

                    if (managerResponse.Ids != null)
                    {
                        var formattedIds = string.Join(", ", managerResponse.Ids);
                        sb.AppendLine($"Result Ids: [{formattedIds}]");
                    }
                }

                if (!string.IsNullOrWhiteSpace(message))
                {
                    sb.AppendLine(message);
                }

                logger.Log(level, sb.ToString());
            }
            finally
            {
            }
        }
    }
}
