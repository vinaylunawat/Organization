namespace Geography.Business.Country.Manager
{
    using Geography.Business.Country.Models;
    using Geography.Business.Country.Validators;
    using Geography.DataAccess;
    using Geography.DataAccess.Repository;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Business.Manager.Command;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class CountryCommandManager : CodeCommandManager<GeographyDbContext, GeographyReadOnlyDbContext, CountryErrorCode, Entity.Entities.Country, CountryCreateModel, CountryUpdateModel>, ICountryCommandManager
    {
        private readonly ICountryQueryRepository _countryQueryRepository;
        private readonly ICountryCommandRepository _countryCommandRepository;
        public CountryCommandManager(
            CountryCreateModelValidator createModelValidator,
            CountryUpdateModelValidator updateModelValidator,
            ILogger<CountryCommandManager> logger,
            IMapper mapper
            , ICountryQueryRepository countryQueryRepository
            , ICountryCommandRepository countryCommandRepository
            )
            : base(countryQueryRepository, countryCommandRepository, createModelValidator, updateModelValidator, logger, mapper, CountryErrorCode.IdDoesNotExist, CountryErrorCode.IdNotUnique, CountryErrorCode.CodeNotUnique)
        {
            _countryQueryRepository = countryQueryRepository;
            _countryCommandRepository = countryCommandRepository;
        }

        protected override async Task<ErrorRecords<CountryErrorCode>> CreateValidationAsync(IList<IIndexedItem<CountryCreateModel>> indexedModels)
        {
            var errorRecords = await base.CreateValidationAsync(indexedModels).ConfigureAwait(false);


            var duplicateCodeCheck = await ValidationHelpers.UniqueValidationAsync(
                  async (keys) =>
                  {
                      var result = await _countryQueryRepository.FetchByAsync(x => keys.Contains(x.IsoCode), item => new IdKey<string>(item.Id, item.IsoCode)).ConfigureAwait(false);
                      return result.ToList();
                  },
                  indexedModels,
                  x => x.Item.IsoCode,
                  CountryErrorCode.IsoCodeNotUnique).ConfigureAwait(false);

            return new ErrorRecords<CountryErrorCode>(errorRecords.Concat(duplicateCodeCheck));
        }

        protected override async Task<ErrorRecords<CountryErrorCode>> UpdateValidationAsync(IList<IIndexedItem<CountryUpdateModel>> indexedModels)
        {
            var errorRecords = await base.UpdateValidationAsync(indexedModels).ConfigureAwait(false);



            var duplicateCodeCheck = await ValidationHelpers.UniqueWithIdValidationAsync(
                async (keys) =>
                {
                    var result = await _countryQueryRepository.FetchByAsync(x => keys.Contains(x.IsoCode), item => new IdKey<string>(item.Id, item.IsoCode)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.IsoCode,
                CountryErrorCode.IsoCodeNotUnique).ConfigureAwait(false);

            return new ErrorRecords<CountryErrorCode>(errorRecords.Concat(duplicateCodeCheck));
        }

        protected async override Task<ErrorRecords<CountryErrorCode>> DeleteValidationAsync<T>(IEnumerable<T> keys, IEnumerable<Entity.Entities.Country> entities)
        {
            var errorRecoreds = await base.DeleteValidationAsync(keys, entities).ConfigureAwait(false);


            //entities.Where(country => country.States != null && country.States.Count > 0).ToList().ForEach(x =>
            //{
            //    if (keys is IEnumerable<string>)
            //    {
            //        var errorMessage = new ErrorMessage<CountryErrorCode>("IsoCode", CountryErrorCode.StateAttached, "State attached to Country, must delete the attached state before deleting the country", x.IsoCode);
            //        var errorRecord = new ErrorRecord<CountryErrorCode>(keys.ToList().FindIndex(key => key.ToString() == x.IsoCode) + 1, errorMessage);
            //        errorRecoreds.Add(errorRecord);
            //    }
            //    else
            //    {
            //        var errorMessage = new ErrorMessage<CountryErrorCode>("Id", CountryErrorCode.StateAttached, "State attached to Country, must delete the attached state before deleting the country", x.Id);
            //        var errorRecord = new ErrorRecord<CountryErrorCode>(keys.ToList().FindIndex(key => Convert.ToInt64(key, null) == x.Id) + 1, errorMessage);
            //        errorRecoreds.Add(errorRecord);
            //    }
            //});
            return errorRecoreds;
        }

        public async Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(string isoCode, params string[] isoCodes)
        {
            try
            {
                EnsureArg.IsNotNull(isoCode, nameof(isoCode));
                return await DeleteByIsoCodeAsync(isoCodes.Prepend(isoCode)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIsoCodeAsync));
                return new ManagerResponse<CountryErrorCode>(ex);
            }
        }

        public async Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(IEnumerable<string> isoCodes)
        {
            try
            {
                EnsureArg.IsNotNull(isoCodes, nameof(isoCodes));
                EnsureArgExtensions.HasItems(isoCodes, nameof(isoCodes));

                return await DeleteByExpressionAsync(isoCodes, x => isoCodes.Contains(x.IsoCode)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIsoCodeAsync));
                return new ManagerResponse<CountryErrorCode>(ex);
            }
        }

        //protected override async Task<IEnumerable<Entity.Entities.Country>> DeleteEntityQueryAsync(Expression<Func<Entity.Entities.Country, bool>> predicate)
        //{
        //    var criteria = new FilterCriteria<Entity.Entities.Country>
        //    {
        //        Predicate = predicate
        //    };
        //    criteria.Includes.Add(ec => ec.Code);

        //    var result = await _countryQueryRepository.GetByCriteriaAsync(criteria).ConfigureAwait(false);
        //    return result;
        //}
    }
}
