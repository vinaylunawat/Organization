namespace Framework.Business
{
    using EnsureThat;
    using FluentValidation.Results;
    using Framework.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ErrorMessage{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public class ErrorMessage<TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ErrorMessage(TErrorCode errorCode, string message)
        {
            EnsureArg.IsNotNullOrWhiteSpace(message, nameof(message));

            PropertyName = string.Empty;
            AttemptedValue = null;
            ErrorCode = errorCode;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ErrorMessage(TErrorCode errorCode, Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            PropertyName = string.Empty;
            ErrorCode = errorCode;
            Message = GenerateMessageFromException(exception);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="attemptedValue">The attemptedValue<see cref="object"/>.</param>
        public ErrorMessage(string propertyName, TErrorCode errorCode, string message, object attemptedValue)
        {
            EnsureArg.IsNotNull(propertyName, nameof(propertyName));
            EnsureArg.IsNotNullOrWhiteSpace(message, nameof(message));

            PropertyName = propertyName;
            ErrorCode = errorCode;
            Message = message;
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ErrorMessage(Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            PropertyName = string.Empty;
            ErrorCode = default;
            Message = GenerateMessageFromException(exception);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        /// <param name="validationFailure">The validationFailure<see cref="ValidationFailure"/>.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage{TErrorCode}"/> class.
        /// </summary>
        protected ErrorMessage()
        {
        }

        /// <summary>
        /// Gets or sets the ErrorCode
        /// Gets the error code..
        /// </summary>
        public TErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// Gets the message..
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the PropertyName
        /// Gets the name of the property..
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the attempted value..
        /// </summary>
        public object AttemptedValue { get; set; }

        /// <summary>
        /// The ToFormattedString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        internal string ToFormattedString()
        {
            return $"{ErrorCode.ToString()} - Property: '{PropertyName}' with value '{AttemptedValue}'. {Message}";
        }

        /// <summary>
        /// The GenerateMessageFromException.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
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

        /// <summary>
        /// The BuildErrorMessageFromException.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
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
