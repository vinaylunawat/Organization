namespace Framework.Service.Extension
{
    using ExceptionLogger;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static void AddProblemDetailsSupport(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
