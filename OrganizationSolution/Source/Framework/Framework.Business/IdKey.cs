namespace Framework.Business
{
    public sealed class IdKey<T>
    {
        public IdKey(long id, T key)
        {
            Id = id;
            Key = key;
        }

        public IdKey(T key)
        {
            Id = null;
            Key = key;
        }

        public long? Id { get; set; }

        public T Key { get; set; }
    }
}
