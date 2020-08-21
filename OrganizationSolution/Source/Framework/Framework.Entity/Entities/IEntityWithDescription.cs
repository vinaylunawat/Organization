namespace Framework.Entity
{
    public interface IEntityWithDescription : IBaseEntity
    {
        string Description { get; set; }
    }
}
