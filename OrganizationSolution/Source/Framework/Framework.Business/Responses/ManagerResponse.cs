namespace Framework.Business
{
    using EnsureThat;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ManagerResponse{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public class ManagerResponse<TErrorCode> : ManagerResponseBase<TErrorCode>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ManagerResponse(TErrorCode errorCode, string message)
            : base(errorCode, message)
        {
            Ids = Array.Empty<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponse(TErrorCode errorCode, Exception exception)
            : base(errorCode, exception)
        {
            Ids = Array.Empty<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="errorRecords">The errorRecords<see cref="ErrorRecords{TErrorCode}"/>.</param>
        public ManagerResponse(ErrorRecords<TErrorCode> errorRecords)
            : base(errorRecords)
        {
            Ids = Array.Empty<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ManagerResponse(Exception exception)
            : base(exception)
        {
            Ids = Array.Empty<long>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        public ManagerResponse(IEnumerable<long> ids)
            : base()
        {
            EnsureArg.IsNotNull(ids, nameof(ids));

            Ids = ids.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerResponse{TErrorCode}"/> class.
        /// </summary>
        public ManagerResponse()
            : base()
        {
            Ids = Array.Empty<long>();
        }

        /// <summary>
        /// Gets or sets the Ids.
        /// </summary>
        public long[] Ids { get; set; }
    }
}
