namespace Framework.Security
{
    using Framework.Constant;
    using Framework.Security.Abstraction;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ServiceClientBase" />.
    /// </summary>
    public class ServiceClientBase : IServiceClientBase
    {
        /// <summary>
        /// Gets the BearerToken.
        /// </summary>
        public string BearerToken { get; private set; }

        /// <summary>
        /// Gets the TenantIds.
        /// </summary>
        public string TenantIds { get; private set; }

        /// <summary>
        /// The SetBearerToken.
        /// </summary>
        /// <param name="token">The token<see cref="string"/>.</param>
        public void SetBearerToken(string token)
        {
            BearerToken = token;
        }

        /// <summary>
        /// The SetTenantId.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="string"/>.</param>
        public void SetTenantId(string tenantIds)
        {
            TenantIds = tenantIds;
        }

        /// <summary>
        /// The CreateHttpRequestMessageAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{HttpRequestMessage}"/>.</returns>
        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(SecurityConstants.AuthenticationScheme, BearerToken);
            msg.Headers.Add(SecurityConstants.TenantIdHeaderKey, TenantIds);
            return Task.FromResult(msg);
        }
    }
}
