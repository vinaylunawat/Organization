namespace Framework.Entity
{
    public abstract class EntityWithIdName : EntityWithId, IEntityWithName
    {
        public EntityWithIdName()
        {
        }

        public EntityWithIdName(string name)
        {
            Name = name;
        }

        public EntityWithIdName(long id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
