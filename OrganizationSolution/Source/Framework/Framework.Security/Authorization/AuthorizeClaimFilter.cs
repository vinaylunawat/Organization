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

    public class AuthorizeClaimFilter : IAsyncAuthorizationFilter
    {
        private readonly Claim _claim;
        private readonly ILogger<AuthorizeClaimFilter> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly double _cacheDurationSeconds = 300;

        private readonly IAuthorizeClient _authorizeClient;

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
            //if (claimsPrincipal.HasCurrentModuleSecurityRight(_claim.Value))
            //{
            //    _logger.LogDebug($"Policy: {_claim.Value} was found in user claims. Action allowed");
            //}
            //else
            //{
            //    //var allowed = await HasRightInAccessToken(context).ConfigureAwait(false);

            //    if (false)
            //    {
            //        context.Result = new ForbidResult();
            //    }
            //}
        }
    }
}
