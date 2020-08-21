namespace Geography.Business.State.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Geography.Business.Business.State.Models;
    using Geography.Business.State.Manager;
    using Geography.DataAccess.Repository;
    using Geography.Entity.Entities;
    using Framework.Business.Manager.Query;
    using Framework.Service.Utilities.Criteria;
    using AutoMapper;
    using DataAccess;
    using Microsoft.Extensions.Logging;

    public class StateQueryManager : CodeQueryManager<GeographyReadOnlyDbContext, Entity.Entities.State, StateReadModel>, IStateQueryManager
    {
        private readonly IStateQueryRepository _stateQueryRepository;

        public StateQueryManager(GeographyReadOnlyDbContext databaseContext, ILogger<StateQueryManager> logger, IMapper mapper, IStateQueryRepository stateQueryRepository)
            : base(stateQueryRepository, logger, mapper)
        {
            _stateQueryRepository = stateQueryRepository;
        }

        protected override Task<IEnumerable<State>> EntityQueryAsync(FilterCriteria<State> filterCriteria)
        {
            filterCriteria.Includes.Add(item => item.Country);            
            return base.EntityQueryAsync(filterCriteria);
        }         
    }
}
