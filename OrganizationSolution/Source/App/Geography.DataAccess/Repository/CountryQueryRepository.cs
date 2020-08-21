namespace Geography.DataAccess.Repository
{
    using Entity.Entities;
    using Framework.DataAccess.Repository;

    public class CountryQueryRepository : GenericQueryRepository<GeographyReadOnlyDbContext, Country>, ICountryQueryRepository
    {
        public CountryQueryRepository(GeographyReadOnlyDbContext dbContext) :
            base(dbContext)
        {            
        }
    }
}
