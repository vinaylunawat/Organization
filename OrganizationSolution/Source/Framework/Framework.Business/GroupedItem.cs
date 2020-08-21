namespace Framework.Business
{
    using System.Collections.Generic;

    public sealed class GroupedItem<TItem, TKey>
    {
        public GroupedItem(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            Items = items;
        }

        public TKey Key { get; set; }

        public IEnumerable<TItem> Items { get; set; }
    }
}
