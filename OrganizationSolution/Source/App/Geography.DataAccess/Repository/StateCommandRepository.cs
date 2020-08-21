namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    public class StateCommandRepository : GenericCommandRepository<GeographyDbContext, GeographyReadOnlyDbContext, State>, IStateCommandRepository
    {
        public StateCommandRepository(GeographyDbContext dbContext, IStateQueryRepository queryRepository)
            : base(dbContext, queryRepository)
        {
        }
    }
}
