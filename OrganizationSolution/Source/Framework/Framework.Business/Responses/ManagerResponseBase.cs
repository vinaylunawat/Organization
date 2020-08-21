﻿namespace Framework.Business
{
    using System;
    using System.Linq;
    using EnsureThat;

    public abstract class ManagerResponseBase<TErrorCode>
        where TErrorCode : struct, Enum
    {
        public ManagerResponseBase(TErrorCode errorCode, string message)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotEmptyOrWhiteSpace(message, nameof(message));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(errorCode, message)
            };
        }

        public ManagerResponseBase(TErrorCode errorCode, Exception exception)
        {
            EnsureArg.IsNotNull<TErrorCode>(errorCode, nameof(errorCode));
            EnsureArg.IsNotNull(exception, nameof(exception));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(errorCode, exception)
            };
        }

        public ManagerResponseBase(ErrorRecords<TErrorCode> errorRecords)
        {
            EnsureArg.IsNotNull(errorRecords, nameof(errorRecords));

            ErrorRecords = errorRecords;
        }

        public ManagerResponseBase(Exception exception)
        {
            EnsureArg.IsNotNull(exception, nameof(exception));

            ErrorRecords = new ErrorRecords<TErrorCode>
            {
                new ErrorRecord<TErrorCode>(exception)
            };
        }

        protected ManagerResponseBase()
        {
            ErrorRecords = new ErrorRecords<TErrorCode>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance has an error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has an error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError
        {
            get { return ErrorRecords.Any(); }
        }

        /// <summary>
        /// Gets the error records.
        /// </summary>
        /// <value>
        /// The error records.
        /// </value>
        public ErrorRecords<TErrorCode> ErrorRecords { get; set; }
    }
}
