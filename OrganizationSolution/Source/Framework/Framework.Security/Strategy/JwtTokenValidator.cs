namespace Framework.Security
{
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="JwtTokenValidator" />.
    /// </summary>
    internal sealed class JwtTokenValidator : IJwtTokenValidator
    {
        /// <summary>
        /// Defines the _jwtTokenHandler.
        /// </summary>
        private readonly IJwtTokenHandler _jwtTokenHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenValidator"/> class.
        /// </summary>
        /// <param name="jwtTokenHandler">The jwtTokenHandler<see cref="IJwtTokenHandler"/>.</param>
        internal JwtTokenValidator(IJwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        /// <summary>
        /// The GetPrincipalFromToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="signingKey">The signingKey<see cref="string"/>.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/>.</returns>
        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
        {
            return _jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false // we check expired tokens here
            });
        }
    }
}
