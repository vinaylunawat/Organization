namespace Framework.Security
{
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="IJwtTokenValidator" />.
    /// </summary>
    public interface IJwtTokenValidator
    {
        /// <summary>
        /// The GetPrincipalFromToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <param name="signingKey">The signingKey<see cref="string"/>.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/>.</returns>
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}
