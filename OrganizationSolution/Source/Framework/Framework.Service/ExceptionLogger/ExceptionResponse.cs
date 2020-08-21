namespace Framework.Service.ExceptionLogger
{
    /// <summary>
    /// Defines the <see cref="ExceptionResponse" />.
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionResponse"/> class.
        /// </summary>
        /// <param name="responseContentType">The responseContentType<see cref="string"/>.</param>
        /// <param name="responseText">The responseText<see cref="string"/>.</param>
        public ExceptionResponse(string responseContentType, string responseText)
        {
            ResponseContentType = responseContentType;
            ResponseText = responseText;
        }

        /// <summary>
        /// Gets the ResponseContentType.
        /// </summary>
        public string ResponseContentType { get; }

        /// <summary>
        /// Gets the ResponseText.
        /// </summary>
        public string ResponseText { get; }
    }
}
