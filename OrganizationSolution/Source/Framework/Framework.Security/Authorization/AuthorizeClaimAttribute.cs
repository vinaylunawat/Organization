namespace Framework.Security.Authorization
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="AuthorizeClaimAttribute" />.
    /// </summary>
    public class AuthorizeClaimAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeClaimAttribute"/> class.
        /// </summary>
        /// <param name="claimValue">The claimValue<see cref="string"/>.</param>
        public AuthorizeClaimAttribute(string claimValue)
            : base(typeof(AuthorizeClaimFilter))
        {
            Arguments = new object[] { new Claim(CustomClaimTypes.SecurityRights, claimValue) };
        }
    }
}
