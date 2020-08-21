namespace Framework.Service.ExceptionLogger
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Framework.Service.Extension;
    using ContentTypes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Service;

    public class ExceptionMiddleware
    {
        private const string ProblemTitle = "An unexpected error occurred!";
        private readonly RequestDelegate _requestDelegate;        
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, IHostEnvironment hostingEnvironment, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

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

        private void LogError(Exception exception)
        {
            _logger.LogCritical(exception, nameof(ExceptionMiddleware));
        }
    }
}
