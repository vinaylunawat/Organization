namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithIdTenantIdName" />.
    /// </summary>
    public abstract class EntityWithIdTenantIdName : EntityWithIdTenantId, IEntityWithName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantIdName"/> class.
        /// </summary>
        public EntityWithIdTenantIdName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantIdName"/> class.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdTenantIdName(long tenantId, string name)
            : base(tenantId)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdTenantIdName"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdTenantIdName(long id, long tenantId, string name)
            : base(id, tenantId)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
