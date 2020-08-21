namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Service;

    public class JsonExceptionContentType : AbstractExceptionContentType
    {
        public override ExceptionResponse CreateExceptionResponse(ProblemDetails problemDetails)
        {
            return new ExceptionResponse(
                SupportedContentTypes.ProblemDetailsJson,
                JsonConvert.SerializeObject(problemDetails));
        }
    }
}
