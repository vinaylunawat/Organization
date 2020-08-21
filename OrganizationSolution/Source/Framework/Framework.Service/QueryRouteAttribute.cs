namespace Framework.Service
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="QueryRouteAttribute" />.
    /// </summary>
    public sealed class QueryRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// Defines the TemplateBase.
        /// </summary>
        private const string TemplateBase = "api/query/";

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRouteAttribute"/> class.
        /// </summary>
        public QueryRouteAttribute()
            : base(TemplateBase + "[controller]")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRouteAttribute"/> class.
        /// </summary>
        /// <param name="templateSuffix">The templateSuffix<see cref="string"/>.</param>
        public QueryRouteAttribute(string templateSuffix)
            : base(TemplateBase + templateSuffix)
        {
        }
    }
}
