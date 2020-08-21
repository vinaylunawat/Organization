namespace Framework.Business
{
    using System.Collections.Generic;

    public interface IApiException
    {
        int StatusCode { get; }

        string Response { get; }

        IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }
    }
}
