namespace Geography.Business.State.Manager
{
    using AutoMapper;
    using DataAccess;
    using Framework.Business;
    using Framework.Business.Manager.Command;
    using Geography.Business.Business.State;
    using Geography.Business.Business.State.Models;
    using Geography.Business.Business.State.Validators;
    using Geography.DataAccess.Repository;
    using Geography.Entity.Entities;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="StateCommandManager" />.
    /// </summary>
    public class StateCommandManager : CodeCommandManager<GeographyDbContext, GeographyReadOnlyDbContext, StateErrorCode, State, StateCreateModel, StateUpdateModel>, IStateCommandManager
    {
        /// <summary>
        /// Defines the _countryQueryRepository.
        /// </summary>
        private readonly ICountryQueryRepository _countryQueryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateCommandManager"/> class.
        /// </summary>
        /// <param name="databaseContext">The databaseContext<see cref="GeographyDbContext"/>.</param>
        /// <param name="createModelValidator">The createModelValidator<see cref="StateCreateModelValidator"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="StateUpdateModelValidator"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{StateCommandManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="countryQueryRepository">The countryQueryRepository<see cref="ICountryQueryRepository"/>.</param>
        /// <param name="stateQueryRepository">The stateQueryRepository<see cref="IStateQueryRepository"/>.</param>
        /// <param name="stateCommandRepository">The stateCommandRepository<see cref="IStateCommandRepository"/>.</param>
        public StateCommandManager(
            GeographyDbContext databaseContext,
            StateCreateModelValidator createModelValidator,
            StateUpdateModelValidator updateModelValidator,
            ILogger<StateCommandManager> logger,
            IMapper mapper, ICountryQueryRepository countryQueryRepository,
            IStateQueryRepository stateQueryRepository,
            IStateCommandRepository stateCommandRepository
            )
            : base(stateQueryRepository, stateCommandRepository, createModelValidator, updateModelValidator, logger, mapper, StateErrorCode.IdDoesNotExist, StateErrorCode.IdNotUnique, StateErrorCode.CodeNotUnique)
        {
            _countryQueryRepository = countryQueryRepository;
        }

        /// <summary>
        /// The CreateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{StateCreateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{StateErrorCode}}"/>.</returns>
        protected override async Task<ErrorRecords<StateErrorCode>> CreateValidationAsync(IList<IIndexedItem<StateCreateModel>> indexedModels)
        {
            var errorRecords = await base.CreateValidationAsync(indexedModels).ConfigureAwait(false);

            var countryIdNotExist = await ValidationHelpers.ExistsValidationAsync(
                async (keys) =>
                {
                    var result = await _countryQueryRepository.FetchByAsync(x => keys.Contains(x.IsoCode), x => new IdKey<string>(x.Id, x.IsoCode)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.CountryCode,
                StateErrorCode.CountryCodeDoesNotExist).ConfigureAwait(false);

            return new ErrorRecords<StateErrorCode>(errorRecords.Concat(countryIdNotExist));
        }

        /// <summary>
        /// The UpdateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{StateUpdateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{StateErrorCode}}"/>.</returns>
        protected override async Task<ErrorRecords<StateErrorCode>> UpdateValidationAsync(IList<IIndexedItem<StateUpdateModel>> indexedModels)
        {
            var errorRecords = await base.UpdateValidationAsync(indexedModels).ConfigureAwait(false);

            var countryIdNotExist = await ValidationHelpers.ExistsValidationAsync(
                 async (keys) =>
                 {
                     var result = await _countryQueryRepository.FetchByAsync(x => keys.Contains(x.IsoCode), x => new IdKey<string>(x.Id, x.IsoCode)).ConfigureAwait(false);
                     return result.ToList();
                 },
                 indexedModels,
                 x => x.Item.CountryCode,
                 StateErrorCode.CountryCodeDoesNotExist).ConfigureAwait(false);

            return new ErrorRecords<StateErrorCode>(errorRecords.Concat(countryIdNotExist));
        }

        /// <summary>
        /// The CreateAndUpdateAfterMapAsync.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{State}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected async Task CreateAndUpdateAfterMapAsync<TModel>(IList<IIndexedItem<TModel>> indexedItems, IList<State> entities)
            where TModel : StateCreateModel
        {
            // change country code to country id and timezone code to timezone id
            var countryCodes = indexedItems.Select(x => x.Item.CountryCode).Distinct().ToList();
            var countryKeys = (await _countryQueryRepository.FetchByAsync(x => countryCodes.Contains(x.IsoCode), x => new IdKey<string>(x.Id, x.IsoCode)).ConfigureAwait(false)).ToList();
            for (int index = 0; index < entities.Count; index++)
            {
                entities[index].CountryId = countryKeys.First(x => x.Key == indexedItems[index].Item.CountryCode).Id.Value;
            }
        }

        /// <summary>
        /// The CreateAfterMapAsync.
        /// </summary>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{StateCreateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{State}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected override async Task CreateAfterMapAsync(IList<IIndexedItem<StateCreateModel>> indexedItems, IList<State> entities)
        {
            await CreateAndUpdateAfterMapAsync(indexedItems, entities).ConfigureAwait(false);
        }

        /// <summary>
        /// The UpdateAfterMapAsync.
        /// </summary>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{StateUpdateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{State}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected override async Task UpdateAfterMapAsync(IList<IIndexedItem<StateUpdateModel>> indexedItems, IList<State> entities)
        {
            await CreateAndUpdateAfterMapAsync(indexedItems, entities).ConfigureAwait(false);
        }
    }
}
