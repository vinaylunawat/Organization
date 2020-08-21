namespace Framework.Entity
{
    public abstract class EntityWithIdTenantId : EntityWithId, IEntityWithTenantId
    {
        public EntityWithIdTenantId()
        {
        }

        public EntityWithIdTenantId(long tenantId)
        {
            TenantId = tenantId;
        }

        public EntityWithIdTenantId(long id, long tenantId)
            : base(id)
        {
            TenantId = tenantId;
        }

        public long TenantId { get; set; }
    }
}
