namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    public interface IStateQueryRepository : IGenericQueryRepository<GeographyReadOnlyDbContext, State>
    {
    }
}
