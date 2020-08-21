namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="IEntityWithDescription" />.
    /// </summary>
    public interface IEntityWithDescription : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        string Description { get; set; }
    }
}
