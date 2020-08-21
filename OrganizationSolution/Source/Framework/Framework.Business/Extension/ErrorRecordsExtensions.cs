namespace Framework.Business.Extension
{
    using EnsureThat;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ErrorRecordsExtensions" />.
    /// </summary>
    public static class ErrorRecordsExtensions
    {
        /// <summary>
        /// The ContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="TErrorCode[]"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool ContainsErrorCode<TErrorCode>(this ErrorRecords<TErrorCode> errorRecords, TErrorCode errorCode, params TErrorCode[] errorCodes)
            where TErrorCode : struct, Enum
        {
            return ContainsErrorCode(errorRecords, errorCodes.Prepend(errorCode));
        }

        /// <summary>
        /// The ContainsErrorCode.
        /// </summary>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        /// <param name="errorCodes">The errorCodes<see cref="IEnumerable{TErrorCode}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool ContainsErrorCode<TErrorCode>(this ErrorRecords<TErrorCode> errorRecords, IEnumerable<TErrorCode> errorCodes)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(errorCodes, nameof(errorCodes));

            return errorRecords.Any(x => x.Errors.Any(y => errorCodes.Contains(y.ErrorCode)));
        }
    }
}
