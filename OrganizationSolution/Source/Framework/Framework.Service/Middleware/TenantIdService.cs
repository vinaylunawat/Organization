namespace Framework.Service
{
    using Framework.Constant;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Defines the <see cref="TenantIdService" />.
    /// </summary>
    public class TenantIdService : BaseService, ITenantIdService
    {
        /// <summary>
        /// Defines the _httpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Defines the _provider.
        /// </summary>
        private readonly ITenantIdProvider _provider;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<TenantIdService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
        /// <param name="provider">The provider<see cref="ITenantIdProvider"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{TenantIdService}"/>.</param>
        public TenantIdService(IHttpContextAccessor httpContextAccessor, ITenantIdProvider provider, ILogger<TenantIdService> logger)
            : base(httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _provider = provider;
            _logger = logger;
        }

        /// <summary>
        /// The SetTenantId.
        /// </summary>
        public void SetTenantId()
        {
            try
            {
                // Validate TenantId Header & Validate Auth Token
                if (!ValidateAuthTokenHeaderExists() && !ValidateTenantIdHeaderExists())
                {
                    return;
                }

                IEnumerable<long> claimTenantIds = new List<long>();
                IEnumerable<long> tenantIds = new List<long>();

                // Validate Auth Token
                if (ValidateAuthTokenHeaderExists())
                {
                    var claims = GetClaims();
                    if (claims.Count() > 0)
                    {
                        claimTenantIds = GetClaimTenantIds(claims);
                    }
                    else
                    {
                        return;
                    }
                }

                // Validate TenantId Header
                if (ValidateTenantIdHeaderExists())
                {
                    tenantIds = GetTenantIds();
                }
                else
                {
                    throw new InvalidOperationException(SecurityConstants.TenantIdRequestValidationMessage);
                }

                // Validates multiple tenant ids
                if (_httpContextAccessor.HttpContext.Request.Method.ToLower() != SecurityConstants.GetMethod.ToLower() && tenantIds.Count() > 1)
                {
                    throw new InvalidOperationException(SecurityConstants.MultipleTenantIdsValidationMessage);
                }

                if (claimTenantIds.Count() > 0)
                {
                    // Set tenant id
                    if (!tenantIds.Any(x => !claimTenantIds.Contains(x)))
                    {
                        _provider.SetTenantId(tenantIds);
                    }
                    else
                    {
                        throw new InvalidOperationException(SecurityConstants.InvalidTenantIdValidationMessage);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (FormatException)
            {
                _logger.LogInformation(SecurityConstants.InvalidTenantIdValidationMessage);
                throw;
            }
        }

        /// <summary>
        /// The GetTenantIds.
        /// </summary>
        /// <param name="claims">The claims<see cref="IEnumerable{Claim}"/>.</param>
        /// <returns>The <see cref="IEnumerable{long}"/>.</returns>
        private IEnumerable<long> GetClaimTenantIds(IEnumerable<Claim> claims)
        {
            var claimValue = claims.Where(x => x.Type == SecurityConstants.TenantIds);
            string[] claimArray = claimValue.Count() > 0
                ? claimValue.FirstOrDefault().Value.Split(',')
                : throw new InvalidOperationException(SecurityConstants.TenantIdRequestValidationMessage);

            if (claimArray == null && claimArray.Count() == 0)
            {
                throw new InvalidOperationException(SecurityConstants.InvalidTenantIdValidationMessage);
            }

            return claimArray.Select(long.Parse).ToArray();
        }

        /// <summary>
        /// The GetTenantIds.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{long}"/>.</returns>
        private IEnumerable<long> GetTenantIds()
        {
            // Get Tenant Ids
            var tenantIdString = _httpContextAccessor.HttpContext.Request.Headers[SecurityConstants.TenantIdHeaderKey].ToString();
            string[] tenantIdStringArray = tenantIdString.Split(',');

            return tenantIdStringArray.Select(long.Parse).ToArray();
        }

        /// <summary>
        /// The ValidateTenantIdHeaderExists.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ValidateTenantIdHeaderExists()
        {
            return _httpContextAccessor.HttpContext.Request.Headers.ContainsKey(SecurityConstants.TenantIdHeaderKey);
        }
    }
}
