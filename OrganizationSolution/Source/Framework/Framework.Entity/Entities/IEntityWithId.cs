namespace Framework.Entity
{
    public interface IEntityWithId : IBaseEntity
    {
        long Id { get; set; }
    }
}
