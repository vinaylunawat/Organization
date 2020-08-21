namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="StateCommandRepository" />.
    /// </summary>
    public class StateCommandRepository : GenericCommandRepository<GeographyDbContext, GeographyReadOnlyDbContext, State>, IStateCommandRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateCommandRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyDbContext"/>.</param>
        /// <param name="queryRepository">The queryRepository<see cref="IStateQueryRepository"/>.</param>
        public StateCommandRepository(GeographyDbContext dbContext, IStateQueryRepository queryRepository)
            : base(dbContext, queryRepository)
        {
        }
    }
}
