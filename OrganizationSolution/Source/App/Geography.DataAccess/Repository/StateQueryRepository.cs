namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="StateQueryRepository" />.
    /// </summary>
    public class StateQueryRepository : GenericQueryRepository<GeographyReadOnlyDbContext, State>, IStateQueryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateQueryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyReadOnlyDbContext"/>.</param>
        public StateQueryRepository(GeographyReadOnlyDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
