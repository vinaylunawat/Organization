namespace Framework.Service
{
    using Microsoft.AspNetCore.Mvc;
    public sealed class QueryRouteAttribute : RouteAttribute
    {
        private const string TemplateBase = "api/query/";

        public QueryRouteAttribute()
            : base(TemplateBase + "[controller]")
        {
        }

        public QueryRouteAttribute(string templateSuffix)
            : base(TemplateBase + templateSuffix)
        {
        }
    }
}
