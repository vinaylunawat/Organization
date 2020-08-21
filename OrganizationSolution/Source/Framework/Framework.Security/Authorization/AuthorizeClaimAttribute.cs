namespace Framework.Security.Authorization
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorizeClaimAttribute : TypeFilterAttribute
    {
        public AuthorizeClaimAttribute(string claimValue)
            : base(typeof(AuthorizeClaimFilter))
        {
            Arguments = new object[] { new Claim(CustomClaimTypes.SecurityRights, claimValue) };
        }
    }
}
