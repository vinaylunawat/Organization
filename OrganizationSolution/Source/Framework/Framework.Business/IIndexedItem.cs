namespace Framework.Business
{
    public interface IIndexedItem<out T>
    {
        int OrdinalPosition { get; }

        T Item { get; }
    }
}
