namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="CountryCommandRepository" />.
    /// </summary>
    public class CountryCommandRepository : GenericCommandRepository<GeographyDbContext, GeographyReadOnlyDbContext, Country>, ICountryCommandRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCommandRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyDbContext"/>.</param>
        /// <param name="queryRepository">The queryRepository<see cref="ICountryQueryRepository"/>.</param>
        public CountryCommandRepository(GeographyDbContext dbContext, ICountryQueryRepository queryRepository)
            : base(dbContext, queryRepository)
        {
        }
    }
}
