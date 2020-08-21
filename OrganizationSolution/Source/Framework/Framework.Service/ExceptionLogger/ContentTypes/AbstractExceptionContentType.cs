namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class AbstractExceptionContentType
    {
        public abstract ExceptionResponse CreateExceptionResponse(ProblemDetails problemDetails);
    }
}
