namespace Geography.Business.Country.Manager
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.Business.Manager.Query;
    using Framework.Service.Utilities.Criteria;
    using Geography.Business.Country.Models;
    using Geography.DataAccess;
    using Geography.DataAccess.Repository;
    using Geography.Entity.Entities;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryQueryManager" />.
    /// </summary>
    public class CountryQueryManager : CodeQueryManager<GeographyReadOnlyDbContext, Country, CountryReadModel>, ICountryQueryManager
    {
        /// <summary>
        /// Defines the _countryRepository.
        /// </summary>
        private readonly ICountryQueryRepository _countryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryQueryManager"/> class.
        /// </summary>
        /// <param name="countryRepository">The countryRepository<see cref="ICountryQueryRepository"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CountryQueryManager}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public CountryQueryManager(ICountryQueryRepository countryRepository, ILogger<CountryQueryManager> logger, IMapper mapper)
            : base(countryRepository, logger, mapper)
        {
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// The GetByName.
        /// </summary>
        /// <param name="countryName">The countryName<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        public async Task<IEnumerable<CountryReadModel>> GetByName(string countryName)
        {
            FilterCriteria<Country> filterCriteria = new FilterCriteria<Country>
            {
                Predicate = item => item.Name == countryName
            };
            var result = await EntityQueryAsync(filterCriteria).ConfigureAwait(false);
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryReadModel>>(result);
        }

        /// <summary>
        /// The GetByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        /// <param name="isoCodes">The isoCodes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        public async Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(string isoCode, params string[] isoCodes)
        {
            EnsureArg.IsNotNullOrEmpty(isoCode, nameof(isoCode));
            return await GetByIsoCodeAsync(isoCodes.Prepend(isoCode)).ConfigureAwait(false);
        }

        /// <summary>
        /// The GetByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCodes">The isoCodes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        public async Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(IEnumerable<string> isoCodes)
        {
            EnsureArg.IsNotNull(isoCodes, nameof(isoCodes));
            EnsureArgExtensions.HasItems(isoCodes, nameof(isoCodes));
            FilterCriteria<Country> filterCriteria = new FilterCriteria<Country>
            {
                Predicate = x => isoCodes.Contains(x.IsoCode)
            };
            var readModels = await GetByExpressionAsync(filterCriteria).ConfigureAwait(false);
            return readModels;
        }

        /// <summary>
        /// The EntityQueryAsync.
        /// </summary>
        /// <param name="filterCriteria">The filterCriteria<see cref="FilterCriteria{Country}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{Country}}"/>.</returns>
        protected override Task<IEnumerable<Country>> EntityQueryAsync(FilterCriteria<Country> filterCriteria)
        {
            filterCriteria.Includes.Add(item => item.States);
            return base.EntityQueryAsync(filterCriteria);
        }
    }
}
