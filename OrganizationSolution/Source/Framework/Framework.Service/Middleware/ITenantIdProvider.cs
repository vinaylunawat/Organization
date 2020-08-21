namespace Framework.Service
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ITenantIdProvider" />.
    /// </summary>
    public interface ITenantIdProvider
    {
        /// <summary>
        /// Gets the tenant ids for the request..
        /// </summary>
        IEnumerable<long> TenantIds { get; }

        /// <summary>
        /// The SetTenantId.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        void SetTenantId(IEnumerable<long> tenantIds);
    }
}
