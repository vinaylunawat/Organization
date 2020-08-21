namespace Framework.Business
{
    /// <summary>
    /// Defines the <see cref="IIndexedItem{out T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IIndexedItem<out T>
    {
        /// <summary>
        /// Gets the OrdinalPosition.
        /// </summary>
        int OrdinalPosition { get; }

        /// <summary>
        /// Gets the Item.
        /// </summary>
        T Item { get; }
    }
}
