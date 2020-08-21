namespace Framework.Entity
{
    public abstract class EntityWithIdTenantIdName : EntityWithIdTenantId, IEntityWithName
    {
        public EntityWithIdTenantIdName()
        {
        }

        public EntityWithIdTenantIdName(long tenantId, string name)
            : base(tenantId)
        {
            Name = name;
        }

        public EntityWithIdTenantIdName(long id, long tenantId, string name)
            : base(id, tenantId)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
