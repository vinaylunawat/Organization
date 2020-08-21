namespace Framework.Security
{
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public interface IJwtTokenHandler
    {
        string WriteToken(JwtSecurityToken jwt);

        string WriteToken(SecurityTokenDescriptor securityTokenDescriptor);
        ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
    }
}
