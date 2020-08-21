namespace Geography.Business.State.Managers
{
    using AutoMapper;
    using DataAccess;
    using Framework.Business.Manager.Query;
    using Framework.Service.Utilities.Criteria;
    using Geography.Business.Business.State.Models;
    using Geography.Business.State.Manager;
    using Geography.DataAccess.Repository;
    using Geography.Entity.Entities;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="StateQueryManager" />.
    /// </summary>
    public class StateQueryManager : CodeQueryManager<GeographyReadOnlyDbContext, Entity.Entities.State, StateReadModel>, IStateQueryManager
    {
        /// <summary>
        /// Defines the _stateQueryRepository.
        /// </summary>
        private readonly IStateQueryRepository _stateQueryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateQueryManager"/> class.
        /// </summary>
        /// <param name="databaseContext">The databaseContext<see cref="GeographyReadOnlyDbContext"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{StateQueryManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="stateQueryRepository">The stateQueryRepository<see cref="IStateQueryRepository"/>.</param>
        public StateQueryManager(GeographyReadOnlyDbContext databaseContext, ILogger<StateQueryManager> logger, IMapper mapper, IStateQueryRepository stateQueryRepository)
            : base(stateQueryRepository, logger, mapper)
        {
            _stateQueryRepository = stateQueryRepository;
        }

        /// <summary>
        /// The EntityQueryAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{State}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{State}}"/>.</returns>
        protected override Task<IEnumerable<State>> EntityQueryAsync(FilterCriteria<State> filterCriteria)
        {
            filterCriteria.Includes.Add(item => item.Country);
            return base.EntityQueryAsync(filterCriteria);
        }
    }
}
