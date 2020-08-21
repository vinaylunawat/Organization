namespace Framework.Service.ExceptionLogger
{
    using ContentTypes;
    using Framework.Service.Extension;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Service;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ExceptionMiddleware" />.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Defines the ProblemTitle.
        /// </summary>
        private const string ProblemTitle = "An unexpected error occurred!";

        /// <summary>
        /// Defines the _requestDelegate.
        /// </summary>
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// Defines the _hostingEnvironment.
        /// </summary>
        private readonly IHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="requestDelegate">The requestDelegate<see cref="RequestDelegate"/>.</param>
        /// <param name="hostingEnvironment">The hostingEnvironment<see cref="IHostEnvironment"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{ExceptionMiddleware}"/>.</param>
        public ExceptionMiddleware(RequestDelegate requestDelegate, IHostEnvironment hostingEnvironment, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// The InvokeAsync.
        /// </summary>
        /// <param name="httpContext">The httpContext<see cref="HttpContext"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext != null)
                {
                    await (_requestDelegate?.Invoke(httpContext)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// The WriteResponse.
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/>.</param>
        /// <param name="exceptionResponse">The exceptionResponse<see cref="ExceptionResponse"/>.</param>
        /// <param name="problemDetails">The problemDetails<see cref="ProblemDetails"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private static async Task WriteResponse(HttpContext context, ExceptionResponse exceptionResponse, ProblemDetails problemDetails)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = exceptionResponse == null
                ? SupportedContentTypes.TextPlain
                : exceptionResponse.ResponseContentType;
            await context.Response
                .WriteAsync(exceptionResponse == null
                    ? problemDetails.ToFormattedString()
                    : exceptionResponse.ResponseText).ConfigureAwait(false);
        }

        /// <summary>
        /// The HandleExceptionAsync.
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ExceptionResponse exceptionResponse = default;
            LogError(exception);
            ProblemDetails problemDetails = CreateProblemDetailsObject(exception);
            if (context.Request.Headers.TryGetValue("Accept", out var acceptContentTypes))
            {
                if (acceptContentTypes.Contains(SupportedContentTypes.Json))
                {
                    exceptionResponse = new JsonExceptionContentType().CreateExceptionResponse(problemDetails);
                }
                else if (acceptContentTypes.Contains(SupportedContentTypes.Xml))
                {
                    exceptionResponse = new XmlExceptionContentType().CreateExceptionResponse(problemDetails);
                }
            }

            await WriteResponse(context, exceptionResponse, problemDetails).ConfigureAwait(false);
        }

        /// <summary>
        /// The CreateProblemDetailsObject.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="ProblemDetails"/>.</returns>
        private ProblemDetails CreateProblemDetailsObject(Exception exception)
        {
            string errorDetail;
            if (_hostingEnvironment.IsDevelopment())
            {
                errorDetail = exception.Demystify().ToString();
            }
            else
            {
                errorDetail = "An error occurred please contact administrator";
            }

            return new ProblemDetails
            {
                Title = ProblemTitle,
                Status = StatusCodes.Status400BadRequest,
                Detail = errorDetail,
                Instance = $"urn:MyOrganization:error:{Guid.NewGuid()}"
            };
        }

        /// <summary>
        /// The LogError.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        private void LogError(Exception exception)
        {
            _logger.LogCritical(exception, nameof(ExceptionMiddleware));
        }
    }
}
