namespace Framework.Entity
{
    /// <summary>
    /// Defines the <see cref="EntityWithIdCode" />.
    /// </summary>
    public abstract class EntityWithIdCode : EntityWithId, IEntityWithCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCode"/> class.
        /// </summary>
        public EntityWithIdCode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCode"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        public EntityWithIdCode(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithIdCode"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        public EntityWithIdCode(long id, string code)
            : base(id)
        {
            Code = code;
        }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public string Code { get; set; }
    }
}
