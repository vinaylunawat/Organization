namespace Framework.Security
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    public sealed class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ILogger<JwtTokenHandler> _logger;

        public JwtTokenHandler(ILogger<JwtTokenHandler> logger)
        {
            if (_jwtSecurityTokenHandler == null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            _logger = logger;
        }

        public string WriteToken(JwtSecurityToken jwt)
        {            
            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }
        public string WriteToken(SecurityTokenDescriptor securityTokenDescriptor)
        {
            SecurityToken securityToken = _jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(securityToken);
        }

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
