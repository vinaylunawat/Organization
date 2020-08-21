namespace Framework.Entity
{
    public abstract class EntityWithIdCodeName : EntityWithIdCode, IEntityWithName
    {
        public EntityWithIdCodeName()
        {
        }

        public EntityWithIdCodeName(string code, string name)
            : base(code)
        {
            Name = name;
        }

        public EntityWithIdCodeName(long id, string code, string name)
            : base(id, code)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
