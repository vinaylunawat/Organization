namespace Framework.Entity
{
    using System;

    /// <summary>
    /// Defines the <see cref="IEntityWithMasterKey" />.
    /// </summary>
    public interface IEntityWithMasterKey : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the MasterKey.
        /// </summary>
        Guid MasterKey { get; set; }
    }
}
