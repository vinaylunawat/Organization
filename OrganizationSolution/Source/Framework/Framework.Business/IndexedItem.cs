namespace Framework.Business
{
    /// <summary>
    /// Defines the <see cref="IndexedItem{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public sealed class IndexedItem<T> : IIndexedItem<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedItem{T}"/> class.
        /// </summary>
        /// <param name="ordinalPosition">The ordinalPosition<see cref="int"/>.</param>
        /// <param name="item">The item<see cref="T"/>.</param>
        public IndexedItem(int ordinalPosition, T item)
        {
            OrdinalPosition = ordinalPosition;
            Item = item;
        }

        /// <summary>
        /// Gets the OrdinalPosition.
        /// </summary>
        public int OrdinalPosition { get; }

        /// <summary>
        /// Gets the Item.
        /// </summary>
        public T Item { get; }
    }
}
