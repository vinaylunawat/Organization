namespace Framework.Business.Models
{
    /// <summary>
    /// Defines the <see cref="IModelWithTenantId" />.
    /// </summary>
    public interface IModelWithTenantId : IModelWithId
    {
        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        long TenantId { get; set; }
    }
}
