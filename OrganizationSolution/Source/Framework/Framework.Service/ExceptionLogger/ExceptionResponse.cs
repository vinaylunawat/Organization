namespace Framework.Service.ExceptionLogger
{
    public class ExceptionResponse
    {
        public ExceptionResponse(string responseContentType, string responseText)
        {
            ResponseContentType = responseContentType;
            ResponseText = responseText;
        }

        public string ResponseContentType { get; }

        public string ResponseText { get; }
    }
}
