namespace Framework.Business
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ErrorMessages{TErrorCode}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    public class ErrorMessages<TErrorCode> : WrapperObject<ErrorMessage<TErrorCode>>
        where TErrorCode : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessages{TErrorCode}"/> class.
        /// </summary>
        public ErrorMessages()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessages{TErrorCode}"/> class.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{ErrorMessage{TErrorCode}}"/>.</param>
        public ErrorMessages(IEnumerable<ErrorMessage<TErrorCode>> models)
            : base(models)
        {
        }
    }
}
