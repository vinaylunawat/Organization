namespace Framework.Business
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IApiException" />.
    /// </summary>
    public interface IApiException
    {
        /// <summary>
        /// Gets the StatusCode.
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        /// Gets the Response.
        /// </summary>
        string Response { get; }

        /// <summary>
        /// Gets the Headers.
        /// </summary>
        IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }
    }
}
