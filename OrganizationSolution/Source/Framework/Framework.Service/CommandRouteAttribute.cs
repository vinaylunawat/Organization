namespace Framework.Service
{
    using Microsoft.AspNetCore.Mvc;

    public class CommandRouteAttribute : RouteAttribute
    {
        private const string TemplateBase = "api/command/";

        public CommandRouteAttribute()
            : base(TemplateBase + "[controller]")
        {
        }

        public CommandRouteAttribute(string templateSuffix)
            : base(TemplateBase + templateSuffix)
        {
        }
    }

}
