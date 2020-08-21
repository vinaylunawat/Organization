namespace Framework.Business
{
    public sealed class IndexedItem<T> : IIndexedItem<T>
    {
        public IndexedItem(int ordinalPosition, T item)
        {
            OrdinalPosition = ordinalPosition;
            Item = item;
        }

        public int OrdinalPosition { get; }

        public T Item { get; }
    }
}
