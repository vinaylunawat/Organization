namespace Framework.Business
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="GroupedItem{TItem, TKey}" />.
    /// </summary>
    /// <typeparam name="TItem">.</typeparam>
    /// <typeparam name="TKey">.</typeparam>
    public sealed class GroupedItem<TItem, TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupedItem{TItem, TKey}"/> class.
        /// </summary>
        /// <param name="key">The key<see cref="TKey"/>.</param>
        /// <param name="items">The items<see cref="IEnumerable{TItem}"/>.</param>
        public GroupedItem(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            Items = items;
        }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the Items.
        /// </summary>
        public IEnumerable<TItem> Items { get; set; }
    }
}
