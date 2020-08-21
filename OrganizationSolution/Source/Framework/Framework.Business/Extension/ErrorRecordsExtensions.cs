namespace Framework.Business.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    public static class ErrorRecordsExtensions
    {
        public static bool ContainsErrorCode<TErrorCode>(this ErrorRecords<TErrorCode> errorRecords, TErrorCode errorCode, params TErrorCode[] errorCodes)
            where TErrorCode : struct, Enum
        {
            return ContainsErrorCode(errorRecords, errorCodes.Prepend(errorCode));
        }

        public static bool ContainsErrorCode<TErrorCode>(this ErrorRecords<TErrorCode> errorRecords, IEnumerable<TErrorCode> errorCodes)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(errorCodes, nameof(errorCodes));

            return errorRecords.Any(x => x.Errors.Any(y => errorCodes.Contains(y.ErrorCode)));
        }
    }
}
