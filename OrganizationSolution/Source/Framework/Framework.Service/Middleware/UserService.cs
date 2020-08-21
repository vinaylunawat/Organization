namespace Framework.Service
{
    using Framework.Constant;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="UserService" />.
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// Defines the _provider.
        /// </summary>
        private readonly IUserServiceProvider _provider;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
        /// <param name="provider">The provider<see cref="IUserServiceProvider"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{UserService}"/>.</param>
        public UserService(IHttpContextAccessor httpContextAccessor, IUserServiceProvider provider, ILogger<UserService> logger)
            : base(httpContextAccessor)
        {
            _provider = provider;
            _logger = logger;
        }

        /// <summary>
        /// The SetUserId.
        /// </summary>
        public void SetUserId()
        {
            var claims = GetClaims();
            var userId = claims.Where(x => x.Type == SecurityConstants.UserId).Select(x => x.Value).Select(long.Parse);

            if (userId.Count() > 0)
            {
                _provider.SetUserId(userId.FirstOrDefault());
            }
        }
    }
}
