namespace Geography.Business.Country.Manager
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Business.Manager.Command;
    using Geography.Business.Country.Models;
    using Geography.Business.Country.Validators;
    using Geography.DataAccess;
    using Geography.DataAccess.Repository;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryCommandManager" />.
    /// </summary>
    public class CountryCommandManager : CodeCommandManager<GeographyDbContext, GeographyReadOnlyDbContext, CountryErrorCode, Entity.Entities.Country, CountryCreateModel, CountryUpdateModel>, ICountryCommandManager
    {
        /// <summary>
        /// Defines the _countryQueryRepository.
        /// </summary>
        private readonly ICountryQueryRepository _countryQueryRepository;

        /// <summary>
        /// Defines the _countryCommandRepository.
        /// </summary>
        private readonly ICountryCommandRepository _countryCommandRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCommandManager"/> class.
        /// </summary>
        /// <param name="createModelValidator">The createModelValidator<see cref="CountryCreateModelValidator"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="CountryUpdateModelValidator"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CountryCommandManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="countryQueryRepository">The countryQueryRepository<see cref="ICountryQueryRepository"/>.</param>
        /// <param name="countryCommandRepository">The countryCommandRepository<see cref="ICountryCommandRepository"/>.</param>
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

        /// <summary>
        /// The CreateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{CountryCreateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{CountryErrorCode}}"/>.</returns>
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

        /// <summary>
        /// The UpdateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{CountryUpdateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{CountryErrorCode}}"/>.</returns>
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

        /// <summary>
        /// The DeleteValidationAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="keys">The keys<see cref="IEnumerable{T}"/>.</param>
        /// <param name="entities">The entities<see cref="IEnumerable{Entity.Entities.Country}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{CountryErrorCode}}"/>.</returns>
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

        /// <summary>
        /// The DeleteByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        /// <param name="isoCodes">The isoCodes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{CountryErrorCode}}"/>.</returns>
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

        /// <summary>
        /// The DeleteByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCodes">The isoCodes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{CountryErrorCode}}"/>.</returns>
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
    }
}
