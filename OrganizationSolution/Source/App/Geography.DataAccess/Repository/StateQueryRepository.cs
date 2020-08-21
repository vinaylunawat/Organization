namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    public class StateQueryRepository : GenericQueryRepository<GeographyReadOnlyDbContext, State>, IStateQueryRepository
    {
        public StateQueryRepository(GeographyReadOnlyDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
