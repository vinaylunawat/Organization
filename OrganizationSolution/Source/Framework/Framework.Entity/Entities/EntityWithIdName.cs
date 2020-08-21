namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithIdName" />.
    /// </summary>
    public abstract class EntityWithIdName : EntityWithId, IEntityWithName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdName"/> class.
        /// </summary>
        public EntityWithIdName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdName"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdName"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdName(long id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
