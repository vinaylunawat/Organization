namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="ICountryCommandRepository" />.
    /// </summary>
    public interface ICountryCommandRepository : IGenericCommandRepository<GeographyDbContext, Country>
    {
    }
}
