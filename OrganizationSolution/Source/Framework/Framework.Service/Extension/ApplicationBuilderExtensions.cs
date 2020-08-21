namespace Framework.Service.Extension
{
    using ExceptionLogger;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Defines the <see cref="ApplicationBuilderExtensions" />.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// The AddProblemDetailsSupport.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        public static void AddProblemDetailsSupport(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
