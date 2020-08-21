namespace Framework.Entity
{
    public abstract class EntityWithIdCode : EntityWithId, IEntityWithCode
    {
        public EntityWithIdCode()
        {
        }

        public EntityWithIdCode(string code)
        {
            Code = code;
        }

        public EntityWithIdCode(long id, string code)
            : base(id)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
