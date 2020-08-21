namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="IEntityWithName" />.
    /// </summary>
    public interface IEntityWithName : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        string Name { get; set; }
    }
}
