namespace Framework.Business
{
    /// <summary>
    /// Defines the <see cref="IdKey{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public sealed class IdKey<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdKey{T}"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="key">The key<see cref="T"/>.</param>
        public IdKey(long id, T key)
        {
            Id = id;
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdKey{T}"/> class.
        /// </summary>
        /// <param name="key">The key<see cref="T"/>.</param>
        public IdKey(T key)
        {
            Id = null;
            Key = key;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public T Key { get; set; }
    }
}
