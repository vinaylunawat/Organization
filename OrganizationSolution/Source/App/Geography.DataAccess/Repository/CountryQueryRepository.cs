namespace Geography.DataAccess.Repository
{
    using Entity.Entities;
    using Framework.DataAccess.Repository;

    /// <summary>
    /// Defines the <see cref="CountryQueryRepository" />.
    /// </summary>
    public class CountryQueryRepository : GenericQueryRepository<GeographyReadOnlyDbContext, Country>, ICountryQueryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryQueryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyReadOnlyDbContext"/>.</param>
        public CountryQueryRepository(GeographyReadOnlyDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
