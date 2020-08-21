namespace Geography.Business.AuthToken.Manager
{
    using Geography.Business.AuthToken.Models;
    using Framework.Business.Manager;
    using Framework.Security;
    using Framework.Security;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthTokenManager : ManagerBase, IAuthTokenManager
    {
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;
        public AuthTokenManager(ILogger<AuthTokenManager> logger, ITokenFactory tokenFactory, IJwtFactory jwtFactory)
            : base(logger)
        {
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
        }

        public async Task<AuthTokenModel> GenerateToken(string userId, string userName)
        {
            var refreshToken = _tokenFactory.GenerateToken();
            var accessToken = await _jwtFactory.GenerateEncodedToken(userId.ToString(), userName, GetCustomClaimData()).ConfigureAwait(false);
            return new AuthTokenModel(accessToken, refreshToken);
        }

        private Dictionary<string, string> GetCustomClaimData()
        {
            return new Dictionary<string, string>()
            {
                { CustomClaimTypes.TenantMasterKey, Guid.Parse("1c56a6d6-9539-4fc5-817c-6bf9478454b2").ToString() },
                { CustomClaimTypes.UserId, "1" },
                { CustomClaimTypes.UserName , "string" },
            };
        }
    }
}
