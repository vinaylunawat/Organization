namespace Framework.Security
{
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="IJwtTokenHandler" />.
    /// </summary>
    public interface IJwtTokenHandler
    {
        /// <summary>
        /// The WriteToken.
        /// </summary>
        /// <param name="jwt">The jwt<see cref="JwtSecurityToken"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string WriteToken(JwtSecurityToken jwt);

        /// <summary>
        /// The WriteToken.
        /// </summary>
        /// <param name="securityTokenDescriptor">The securityTokenDescriptor<see cref="SecurityTokenDescriptor"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string WriteToken(SecurityTokenDescriptor securityTokenDescriptor);

        /// <summary>
        /// The ValidateToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="tokenValidationParameters">The tokenValidationParameters<see cref="TokenValidationParameters"/>.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/>.</returns>
        ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
    }
}
