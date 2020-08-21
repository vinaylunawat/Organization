namespace Framework.Service
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Defines the <see cref="TenantIdMiddlewareExtensions" />.
    /// </summary>
    public static class TenantIdMiddlewareExtensions
    {
        /// <summary>
        /// The AddTenantIdMiddleware.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder AddTenantIdMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantIdMiddleware>();
            return app;
        }
    }
}
