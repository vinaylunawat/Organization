namespace Framework.Entity
{
    public interface IEntityWithTenantId : IBaseEntity
    {
        long TenantId { get; set; }
    }
}
