namespace Framework.Security.Abstraction
{
    /// <summary>
    /// Defines the <see cref="IServiceClientBase" />.
    /// </summary>
    public interface IServiceClientBase
    {
        /// <summary>
        /// The SetBearerToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        void SetBearerToken(string token);

        /// <summary>
        /// The SetTenantId.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="string"/>.</param>
        void SetTenantId(string tenantIds);
    }
}
