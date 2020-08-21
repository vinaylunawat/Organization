namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="IEntityWithCode" />.
    /// </summary>
    public interface IEntityWithCode : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        string Code { get; set; }
    }
}
