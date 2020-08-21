namespace Geography.Business.State.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Geography.Business.Business.State;
    using Geography.Business.Business.State.Validators;
    using AutoMapper;
    using DataAccess;
    using Framework.Business;
    using Microsoft.Extensions.Logging;
    using Geography.Business.Business.State.Models;
    using Geography.Entity.Entities;
    using Framework.Business.Manager.Command;
    using Geography.DataAccess.Repository;

    public class StateCommandManager : CodeCommandManager<GeographyDbContext, GeographyReadOnlyDbContext, StateErrorCode, State, StateCreateModel, StateUpdateModel>, IStateCommandManager
    {
        private readonly ICountryQueryRepository _countryQueryRepository;
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

        protected override async Task CreateAfterMapAsync(IList<IIndexedItem<StateCreateModel>> indexedItems, IList<State> entities)
        {
            await CreateAndUpdateAfterMapAsync(indexedItems, entities).ConfigureAwait(false);
        }

        protected override async Task UpdateAfterMapAsync(IList<IIndexedItem<StateUpdateModel>> indexedItems, IList<State> entities)
        {
            await CreateAndUpdateAfterMapAsync(indexedItems, entities).ConfigureAwait(false);
        }
    }
}
