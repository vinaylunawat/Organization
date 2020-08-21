namespace Framework.Business
{
    using EnsureThat;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ManagerResponseBase{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public abstract class ManagerResponseBase<TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseBase{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ManagerResponseBase(TErrorCode errorCode, string message)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotEmptyOrWhiteSpace(message, nameof(message));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(errorCode, message)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseBase{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponseBase(TErrorCode errorCode, Exception exception)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotNull(exception, nameof(exception));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(errorCode, exception)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseBase{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        public ManagerResponseBase(ErrorRecords<TErrorCode> errorRecords)
        {
            EnsureArg.IsNotNull(errorRecords, nameof(errorRecords));

            ErrorRecords = errorRecords;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseBase{TErrorCode}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponseBase(Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(exception)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseBase{TErrorCode}"/> class.
        /// </summary>
        protected ManagerResponseBase()
        {
            ErrorRecords = new ErrorRecords<TErrorCode>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance has an error..
        /// </summary>
        public bool HasError
        {
            get { return ErrorRecords.Any(); }
        }

        /// <summary>
        /// Gets or sets the ErrorRecords
        /// Gets the error records..
        /// </summary>
        public ErrorRecords<TErrorCode> ErrorRecords { get; set; }
    }
}
