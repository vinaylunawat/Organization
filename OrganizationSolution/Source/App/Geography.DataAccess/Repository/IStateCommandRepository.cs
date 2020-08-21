namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="IStateCommandRepository" />.
    /// </summary>
    public interface IStateCommandRepository : IGenericCommandRepository<GeographyDbContext, State>
    {
    }
}
