namespace Geography.DataAccess.Repository
{
    using Geography.Entity.Entities;
    using Framework.DataAccess.Repository;

    public interface ICountryQueryRepository : IGenericQueryRepository<GeographyReadOnlyDbContext, Country>
    {
    }   
}
