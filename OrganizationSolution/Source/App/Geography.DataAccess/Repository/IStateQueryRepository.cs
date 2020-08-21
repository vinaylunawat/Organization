namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="IStateQueryRepository" />.
    /// </summary>
    public interface IStateQueryRepository : IGenericQueryRepository<GeographyReadOnlyDbContext, State>
    {
    }
}
