namespace Framework.Business
{
    using EnsureThat;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ManagerResponseTyped{TErrorCode, TResult}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TResult">.</typeparam>
    public class ManagerResponseTyped<TErrorCode, TResult> : ManagerResponseBase<TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ManagerResponseTyped(TErrorCode errorCode, string message)
            : base(errorCode, message)
        {
            Results = Array.Empty<TResult>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponseTyped(TErrorCode errorCode, Exception exception)
        : base(errorCode, exception)
        {
            Results = Array.Empty<TResult>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        public ManagerResponseTyped(ErrorRecords<TErrorCode> errorRecords)
            : base(errorRecords)
        {
            Results = Array.Empty<TResult>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponseTyped(Exception exception)
            : base(exception)
        {
            Results = Array.Empty<TResult>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="result">The result<see cref="TResult"/>.</param>
        /// <param name="results">The results<see cref="TResult[]"/>.</param>
        public ManagerResponseTyped(TResult result, params TResult[] results)
        {
            EnsureArg.IsTrue(result != null, nameof(result));

            Results = results.Prepend(result).ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        /// <param name="results">The results<see cref="IEnumerable{TResult}"/>.</param>
        public ManagerResponseTyped(IEnumerable<TResult> results)
            : base()
        {
            EnsureArg.IsNotNull(results, nameof(results));

            Results = results.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponseTyped{TErrorCode, TResult}"/> class.
        /// </summary>
        public ManagerResponseTyped()
            : base()
        {
            Results = Array.Empty<TResult>();
        }

        /// <summary>
        /// Gets or sets the Results.
        /// </summary>
        public TResult[] Results { get; set; }
    }
}
