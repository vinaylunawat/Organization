namespace Framework.Service
{
    using Framework.Constant;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="BaseService" />.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Defines the _httpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// The GetClaims.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Claim}"/>.</returns>
        protected IEnumerable<Claim> GetClaims()
        {
            if (ValidateAuthTokenHeaderExists())
            {
                var tokenString = _httpContextAccessor.HttpContext.Request.Headers[SecurityConstants.AuthorizationHeaderKey].ToString();
                if (!tokenString.ToLower().Contains(SecurityConstants.AuthenticationScheme.ToLower()))
                {
                    return new List<Claim>();
                }
                string[] stringArray = tokenString.Split(" ");
                if (stringArray.Count() == 0 || stringArray.Count() == 1)
                {
                    if (stringArray[0].ToLower() == SecurityConstants.AuthenticationScheme.ToLower())
                    {
                        return new List<Claim>();
                    }
                    throw new InvalidOperationException(SecurityConstants.AuthTokenRequestValidationMessage);
                }

                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(stringArray[1]))
                {
                    // Deserialize auth token to claims
                    var authToken = handler.ReadJwtToken(stringArray[1]);

                    return authToken.Claims;
                }
                else
                {
                    throw new InvalidOperationException(SecurityConstants.AuthTokenRequestValidationMessage);
                }
            }
            return new List<Claim>();
        }

        /// <summary>
        /// The ValidateAuthTokenHeaderExists.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool ValidateAuthTokenHeaderExists()
        {
            return _httpContextAccessor.HttpContext.Request.Headers.ContainsKey(SecurityConstants.AuthorizationHeaderKey);
        }
    }
}
