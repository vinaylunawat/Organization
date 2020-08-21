namespace Framework.Entity
{
    using System;

    public interface IEntityWithMasterKey : IBaseEntity
    {
        Guid MasterKey { get; set; }
    }
}
