namespace Framework.Business
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Framework.Configuration;
    using EnsureThat;
    using FluentValidation.Results;
    using Newtonsoft.Json;

    public class ErrorMessage<TErrorCode>
        where TErrorCode : struct, Enum
    {
        public ErrorMessage(TErrorCode errorCode, string message)
        {
            EnsureArg.IsNotNullOrWhiteSpace(message, nameof(message));

            PropertyName = string.Empty;
            AttemptedValue = null;
            ErrorCode = errorCode;
            Message = message;
        }

        public ErrorMessage(TErrorCode errorCode, Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            PropertyName = string.Empty;
            ErrorCode = errorCode;
            Message = GenerateMessageFromException(exception);
        }

        public ErrorMessage(string propertyName, TErrorCode errorCode, string message, object attemptedValue)
        {
            EnsureArg.IsNotNull(propertyName, nameof(propertyName));
            EnsureArg.IsNotNullOrWhiteSpace(message, nameof(message));

            PropertyName = propertyName;
            ErrorCode = errorCode;
            Message = message;
            AttemptedValue = attemptedValue;
        }

        public ErrorMessage(Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            PropertyName = string.Empty;
            ErrorCode = default;
            Message = GenerateMessageFromException(exception);
        }

        public ErrorMessage(ValidationFailure validationFailure)
        {
            EnsureArg.IsNotNull(validationFailure, nameof(validationFailure));

            PropertyName = validationFailure.PropertyName;

            if (Enum.TryParse(typeof(TErrorCode), validationFailure.ErrorCode, out object errorCode))
            {
                ErrorCode = (TErrorCode)errorCode;
            }
            else
            {
                throw new InvalidOperationException($"Could not parse an error code enumeration of type {typeof(TErrorCode).Name} with a value for {validationFailure.ErrorCode}.");
            }

            AttemptedValue = validationFailure.AttemptedValue?.ToString();
            Message = validationFailure.ErrorMessage;
        }

        protected ErrorMessage()
        {
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public TErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the attempted value.
        /// </summary>
        /// <value>
        /// The attempted value.
        /// </value>
        public object AttemptedValue { get; set; }

        internal string ToFormattedString()
        {
            return $"{ErrorCode.ToString()} - Property: '{PropertyName}' with value '{AttemptedValue}'. {Message}";
        }

        private static string GenerateMessageFromException(Exception exception)
        {
            StringBuilder strBuilder = new StringBuilder();

            string message;
            if (ApplicationConfiguration.IsDevelopment)
            {
                message = exception.Demystify().ToString();
            }
            else
            {
                message = exception.Message;
            }

            var errorMessage = BuildErrorMessageFromException(exception);

            strBuilder.Append(message)
                      .Append(errorMessage);

            return strBuilder.ToString().Replace(Environment.NewLine, " ", StringComparison.OrdinalIgnoreCase);
        }

        private static string BuildErrorMessageFromException(Exception exception)
        {
            try
            {
                var sb = new StringBuilder();

                var resultProperty = exception.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    sb.Append("Result: ");
                    var result = resultProperty.GetValue(exception);
                    if (result != null)
                    {
                        sb.Append(JsonConvert.SerializeObject(result, Formatting.Indented));
                    }
                }

                return sb.ToString();
            }
            finally
            {
            }
        }
    }
}
