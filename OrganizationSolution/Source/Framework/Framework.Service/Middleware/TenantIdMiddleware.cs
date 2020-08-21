namespace Framework.Service
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TenantIdMiddleware" />.
    /// </summary>
    public class TenantIdMiddleware
    {
        /// <summary>
        /// Defines the _next.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdMiddleware"/> class.
        /// </summary>
        /// <param name="next">A <see cref="RequestDelegate"/> containing the next request delegate to process.</param>
        public TenantIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The InvokeAsync.
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/>.</param>
        /// <param name="tenantIdService">The tenantIdService<see cref="ITenantIdService"/>.</param>
        /// <param name="userService">The userService<see cref="IUserService"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task InvokeAsync(HttpContext context, ITenantIdService tenantIdService, IUserService userService)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            tenantIdService.SetTenantId();
            userService.SetUserId();
            await _next(context).ConfigureAwait(false);
        }
    }
}
