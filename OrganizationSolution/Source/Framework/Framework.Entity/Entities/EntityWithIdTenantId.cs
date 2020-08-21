namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithIdTenantId" />.
    /// </summary>
    public abstract class EntityWithIdTenantId : EntityWithId, IEntityWithTenantId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantId"/> class.
        /// </summary>
        public EntityWithIdTenantId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantId"/> class.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        public EntityWithIdTenantId(long tenantId)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantId"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        public EntityWithIdTenantId(long id, long tenantId)
            : base(id)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        public long TenantId { get; set; }
    }
}
