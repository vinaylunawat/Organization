namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    public interface ICountryCommandRepository : IGenericCommandRepository<GeographyDbContext, Country>
    {
    }
}
