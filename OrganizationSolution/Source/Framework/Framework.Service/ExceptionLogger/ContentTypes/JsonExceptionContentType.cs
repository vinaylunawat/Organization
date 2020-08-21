namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Service;

    /// <summary>
    /// Defines the <see cref="JsonExceptionContentType" />.
    /// </summary>
    public class JsonExceptionContentType : AbstractExceptionContentType
    {
        /// <summary>
        /// The CreateExceptionResponse.
        /// </summary>
        /// <param name="problemDetails">The problemDetails<see cref="ProblemDetails"/>.</param>
        /// <returns>The <see cref="ExceptionResponse"/>.</returns>
        public override ExceptionResponse CreateExceptionResponse(ProblemDetails problemDetails)
        {
            return new ExceptionResponse(
                SupportedContentTypes.ProblemDetailsJson,
                JsonConvert.SerializeObject(problemDetails));
        }
    }
}
