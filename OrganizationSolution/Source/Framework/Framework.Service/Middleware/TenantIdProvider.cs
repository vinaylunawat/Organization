namespace Framework.Service
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="TenantIdProvider" />.
    /// </summary>
    public class TenantIdProvider : ITenantIdProvider
    {
        /// <summary>
        /// Defines the MemoryCacheLock.
        /// </summary>
        private static readonly ReaderWriterLockSlim MemoryCacheLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the TenantIds.
        /// </summary>
        public IEnumerable<long> TenantIds { get; private set; }

        /// <summary>
        /// The SetTenantId.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        public void SetTenantId(IEnumerable<long> tenantIds)
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    if (TenantIds == default)
                    {
                        TenantIds = tenantIds;
                    }
                }
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }
    }
}
