namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="AbstractExceptionContentType" />.
    /// </summary>
    public abstract class AbstractExceptionContentType
    {
        /// <summary>
        /// The CreateExceptionResponse.
        /// </summary>
        /// <param name="problemDetails">The problemDetails<see cref="ProblemDetails"/>.</param>
        /// <returns>The <see cref="ExceptionResponse"/>.</returns>
        public abstract ExceptionResponse CreateExceptionResponse(ProblemDetails problemDetails);
    }
}
