namespace Framework.Security
{
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="JwtTokenHandler" />.
    /// </summary>
    public sealed class JwtTokenHandler : IJwtTokenHandler
    {
        /// <summary>
        /// Defines the _jwtSecurityTokenHandler.
        /// </summary>
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<JwtTokenHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{JwtTokenHandler}"/>.</param>
        public JwtTokenHandler(ILogger<JwtTokenHandler> logger)
        {
            if (_jwtSecurityTokenHandler == null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            _logger = logger;
        }

        /// <summary>
        /// The WriteToken.
        /// </summary>
        /// <param name="jwt">The jwt<see cref="JwtSecurityToken"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string WriteToken(JwtSecurityToken jwt)
        {
            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }

        /// <summary>
        /// The WriteToken.
        /// </summary>
        /// <param name="securityTokenDescriptor">The securityTokenDescriptor<see cref="SecurityTokenDescriptor"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string WriteToken(SecurityTokenDescriptor securityTokenDescriptor)
        {
            SecurityToken securityToken = _jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        /// <summary>
        /// The ValidateToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="tokenValidationParameters">The tokenValidationParameters<see cref="TokenValidationParameters"/>.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/>.</returns>
        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception e)
            {
                _logger.LogError($"Token validation failed: {e.Message}");
                return null;
            }
        }
    }
}
