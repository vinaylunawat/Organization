namespace Framework.Security
{
    using System.Security.Claims;
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}
