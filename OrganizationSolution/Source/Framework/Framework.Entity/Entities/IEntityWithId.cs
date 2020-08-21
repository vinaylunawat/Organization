namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="IEntityWithId" />.
    /// </summary>
    public interface IEntityWithId : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        long Id { get; set; }
    }
}
