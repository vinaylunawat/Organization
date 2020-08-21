namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="IEntityWithTenantId" />.
    /// </summary>
    public interface IEntityWithTenantId : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        long TenantId { get; set; }
    }
}
