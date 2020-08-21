namespace Framework.Security
{
    using Framework.Security.Authorization;
    using IdentityModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="ClaimsPrincipalExtensions" />.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// The GetPreferredUserName.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetPreferredUserName(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(JwtClaimTypes.PreferredUserName)?.Value;
        }

        /// <summary>
        /// The GetLoginProviderCode.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLoginProviderCode(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(CustomClaimTypes.LoginProviderCode)?.Value;
        }

        /// <summary>
        /// The GetLoginProviderTypeCode.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLoginProviderTypeCode(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(CustomClaimTypes.LoginProviderTypeCode)?.Value;
        }

        /// <summary>
        /// The GetUserId.
        /// </summary>
        /// <param name="controller">The controller<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long GetUserId(this ControllerBase controller)
        {
            var subjectId = string.Empty;
            var parseUser = controller.Request.Headers.TryGetValue("UserId", out StringValues user);
            if (parseUser)
            {
                subjectId = user.First();
            }

            var parsedState = long.TryParse(subjectId, out var userId);
            if (!parsedState)
            {
                throw new InvalidOperationException($"Invalid SubjectId");
            }

            return userId;
        }

        /// <summary>
        /// The GetTenantId.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long GetTenantId(this ControllerBase controllerBase)
        {
            var claimValue = controllerBase.User.FindFirst(CustomClaimTypes.TenantIds)?.Value;

            var parsedState = long.TryParse(claimValue, out var id);

            if (!parsedState)
            {
                throw new InvalidOperationException($"Invalid Tenant Id");
            }

            return id;
        }

        /// <summary>
        /// The GetCompanyId.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long GetCompanyId(this ControllerBase controllerBase)
        {
            var claimValue = controllerBase.User.FindFirst(CustomClaimTypes.CompanyId)?.Value;

            var parsedState = long.TryParse(claimValue, out var id);

            if (!parsedState)
            {
                throw new InvalidOperationException($"Invalid Company Id");
            }

            return id;
        }

        /// <summary>
        /// The GetCompanyMasterKey.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="Guid"/>.</returns>
        public static Guid GetCompanyMasterKey(this ControllerBase controllerBase)
        {
            var claimValue = controllerBase.User.FindFirst(CustomClaimTypes.CompanyMasterKey)?.Value;

            var parsedState = Guid.TryParse(claimValue, out var masterKey);

            if (!parsedState)
            {
                throw new InvalidOperationException($"Invalid Company Master Key");
            }

            return masterKey;
        }

        /// <summary>
        /// The GetUserLanguageIsoCode.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetUserLanguageIsoCode(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.LanguageIsoCode);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.LanguageIsoCode} claim is missing");
            }

            return claim.Value;
        }

        /// <summary>
        /// The GetUserFormatIsoCode.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetUserFormatIsoCode(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.FormatIsoCode);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.FormatIsoCode} claim is missing");
            }

            return claim.Value;
        }

        /// <summary>
        /// The HasCurrentModuleSecurityRight.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <param name="rightCode">The rightCode<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool HasCurrentModuleSecurityRight(this ControllerBase controllerBase, string rightCode)
        {
            return controllerBase.User.HasCurrentModuleSecurityRight(rightCode);
        }

        /// <summary>
        /// The GetCurrentModuleSecurityRights.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="HashSet{SecurityRightIdentifierModel}"/>.</returns>
        public static HashSet<SecurityRightIdentifierModel> GetCurrentModuleSecurityRights(this ControllerBase controllerBase)
        {
            return controllerBase.User.GetCurrentModuleSecurityRights();
        }

        /// <summary>
        /// The GetUserTimeZone.
        /// </summary>
        /// <param name="controllerBase">The controllerBase<see cref="ControllerBase"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetUserTimeZone(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.TimeZone);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.TimeZone} claim is missing");
            }

            return claim.Value;
        }

        /// <summary>
        /// The HasCurrentModuleSecurityRight.
        /// </summary>
        /// <param name="claimsPrincipal">The claimsPrincipal<see cref="ClaimsPrincipal"/>.</param>
        /// <param name="rightCode">The rightCode<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        internal static bool HasCurrentModuleSecurityRight(this ClaimsPrincipal claimsPrincipal, string rightCode)
        {
            var rights = claimsPrincipal.GetCurrentModuleSecurityRights();
            if (rights == null)
            {
                return false;
            }

            return rights.Any(x => x.SecurityRightCode == rightCode);
        }

        /// <summary>
        /// The GetCurrentModuleSecurityRights.
        /// </summary>
        /// <param name="claimsPrincipal">The claimsPrincipal<see cref="ClaimsPrincipal"/>.</param>
        /// <returns>The <see cref="HashSet{SecurityRightIdentifierModel}"/>.</returns>
        internal static HashSet<SecurityRightIdentifierModel> GetCurrentModuleSecurityRights(this ClaimsPrincipal claimsPrincipal)
        {
            var rightsClaim = claimsPrincipal.FindFirst(CustomClaimTypes.SecurityRights)?.Value;

            if (rightsClaim == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<HashSet<SecurityRightIdentifierModel>>(rightsClaim);
        }
    }
}
