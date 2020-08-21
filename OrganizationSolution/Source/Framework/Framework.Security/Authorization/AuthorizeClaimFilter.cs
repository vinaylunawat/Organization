namespace Framework.Security.Authorization
{
    using EnsureThat;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using System.Security.Claims;
    using System.Threading.Tasks;

    // todo we will refector this class & method
    /// <summary>
    /// Defines the <see cref="AuthorizeClaimFilter" />.
    /// </summary>
    public class AuthorizeClaimFilter : IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Defines the _claim.
        /// </summary>
        private readonly Claim _claim;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<AuthorizeClaimFilter> _logger;

        /// <summary>
        /// Defines the _memoryCache.
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Defines the _cacheDurationSeconds.
        /// </summary>
        private readonly double _cacheDurationSeconds = 300;

        /// <summary>
        /// Defines the _authorizeClient.
        /// </summary>
        private readonly IAuthorizeClient _authorizeClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeClaimFilter"/> class.
        /// </summary>
        /// <param name="claim">The claim<see cref="Claim"/>.</param>
        /// <param name="authorizeClient">The authorizeClient<see cref="IAuthorizeClient"/>.</param>
        /// <param name="memoryCache">The memoryCache<see cref="IMemoryCache"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{AuthorizeClaimFilter}"/>.</param>
        public AuthorizeClaimFilter(Claim claim, IAuthorizeClient authorizeClient, IMemoryCache memoryCache, ILogger<AuthorizeClaimFilter> logger)
        {
            EnsureArg.IsNotNull(claim, nameof(claim));
            EnsureArg.IsNotNull(authorizeClient, nameof(authorizeClient));
            EnsureArg.IsNotNull(memoryCache, nameof(memoryCache));
            EnsureArg.IsNotNull(logger, nameof(logger));

            _claim = claim;
            _authorizeClient = authorizeClient;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        /// <summary>
        /// The OnAuthorizationAsync.
        /// </summary>
        /// <param name="context">The context<see cref="AuthorizationFilterContext"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var claimsPrincipal = context.HttpContext.User;

            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                _logger.LogDebug($"User not Authenticated! Did you forget to decorate [Authorize] attribute");
            }

            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
