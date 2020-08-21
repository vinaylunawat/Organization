namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithIdCodeName" />.
    /// </summary>
    public abstract class EntityWithIdCodeName : EntityWithIdCode, IEntityWithName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCodeName"/> class.
        /// </summary>
        public EntityWithIdCodeName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCodeName"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdCodeName(string code, string name)
            : base(code)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCodeName"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public EntityWithIdCodeName(long id, string code, string name)
            : base(id, code)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
