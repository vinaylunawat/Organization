namespace Framework.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Framework.Security.Authorization;
    using IdentityModel;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetPreferredUserName(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(JwtClaimTypes.PreferredUserName)?.Value;
        }

        public static string GetLoginProviderCode(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(CustomClaimTypes.LoginProviderCode)?.Value;
        }

        public static string GetLoginProviderTypeCode(this ControllerBase controllerBase)
        {
            return controllerBase.User.FindFirst(CustomClaimTypes.LoginProviderTypeCode)?.Value;
        }

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

        public static string GetUserLanguageIsoCode(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.LanguageIsoCode);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.LanguageIsoCode} claim is missing");
            }

            return claim.Value;
        }

        public static string GetUserFormatIsoCode(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.FormatIsoCode);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.FormatIsoCode} claim is missing");
            }

            return claim.Value;
        }

        public static bool HasCurrentModuleSecurityRight(this ControllerBase controllerBase, string rightCode)
        {
            return controllerBase.User.HasCurrentModuleSecurityRight(rightCode);
        }

        public static HashSet<SecurityRightIdentifierModel> GetCurrentModuleSecurityRights(this ControllerBase controllerBase)
        {
            return controllerBase.User.GetCurrentModuleSecurityRights();
        }

        public static string GetUserTimeZone(this ControllerBase controllerBase)
        {
            var claim = controllerBase.User.FindFirst(CustomClaimTypes.TimeZone);

            if (claim == null)
            {
                throw new InvalidOperationException($"{CustomClaimTypes.TimeZone} claim is missing");
            }

            return claim.Value;
        }         

        internal static bool HasCurrentModuleSecurityRight(this ClaimsPrincipal claimsPrincipal, string rightCode)
        {
            var rights = claimsPrincipal.GetCurrentModuleSecurityRights();
            if (rights == null)
            {
                return false;
            }

            return rights.Any(x => x.SecurityRightCode == rightCode);
        }

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
