namespace Framework.Business.Extension
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public static class LoggerExtensions
    {
        public static void LogTrace<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Trace, managerResponse, actionName, message);
        }

        public static void LogDebug<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Debug, managerResponse, actionName, message);
        }

        public static void LogInformation<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Information, managerResponse, actionName, message);
        }

        public static void LogWarning<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Warning, managerResponse, actionName, message);
        }

        public static void LogError<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Error, managerResponse, actionName, message);
        }

        public static void LogCritical<TErrorCode>(this ILogger logger, ManagerResponse<TErrorCode> managerResponse, string actionName = null, string message = null)
            where TErrorCode : struct, Enum
        {
            LogManagerResponseHelper(logger, LogLevel.Critical, managerResponse, actionName, message);
        }

        public static void LogTrace<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Trace, apiException, message, args);
        }

        public static void LogDebug<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Debug, apiException, message, args);
        }

        public static void LogInformation<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Information, apiException, message, args);
        }

        public static void LogWarning<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Warning, apiException, message, args);
        }

        public static void LogError<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Error, apiException, message, args);
        }

        public static void LogCritical<TExceptionType>(this ILogger logger, TExceptionType apiException, string message, params object[] args)
            where TExceptionType : Exception, IApiException
        {
            LogApiExceptionHelper(logger, LogLevel.Critical, apiException, message, args);
        }

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
