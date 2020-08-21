namespace Framework.Service
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="CommandRouteAttribute" />.
    /// </summary>
    public class CommandRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// Defines the TemplateBase.
        /// </summary>
        private const string TemplateBase = "api/command/";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRouteAttribute"/> class.
        /// </summary>
        public CommandRouteAttribute()
            : base(TemplateBase + "[controller]")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRouteAttribute"/> class.
        /// </summary>
        /// <param name="templateSuffix">The templateSuffix<see cref="string"/>.</param>
        public CommandRouteAttribute(string templateSuffix)
            : base(TemplateBase + templateSuffix)
        {
        }
    }
}
