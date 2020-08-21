namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    public interface IStateCommandRepository : IGenericCommandRepository<GeographyDbContext, State>
    {
    }
}
