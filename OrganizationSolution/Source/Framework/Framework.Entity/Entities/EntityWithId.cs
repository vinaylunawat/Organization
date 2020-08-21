namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithId" />.
    /// </summary>
    public abstract class EntityWithId : AuditableEntity, IEntityWithId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithId"/> class.
        /// </summary>
        public EntityWithId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithId"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        public EntityWithId(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long Id { get; set; }
    }
}
