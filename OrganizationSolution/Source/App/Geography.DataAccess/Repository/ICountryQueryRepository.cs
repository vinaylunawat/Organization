namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="ICountryQueryRepository" />.
    /// </summary>
    public interface ICountryQueryRepository : IGenericQueryRepository<GeographyReadOnlyDbContext, Country>
    {
    }
}
