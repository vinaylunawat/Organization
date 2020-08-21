namespace Geography.DataAccess.Repository
{
    using Geography.Entity.Entities;
    using Framework.DataAccess.Repository;    

    public class CountryCommandRepository : GenericCommandRepository<GeographyDbContext, GeographyReadOnlyDbContext, Country>, ICountryCommandRepository
    {
        public CountryCommandRepository(GeographyDbContext dbContext, ICountryQueryRepository queryRepository)
            : base(dbContext, queryRepository)
        {            
        }         
    }
}
