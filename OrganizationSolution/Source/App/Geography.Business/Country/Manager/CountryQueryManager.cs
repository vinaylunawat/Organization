namespace Geography.Business.Country.Manager
{
    using Geography.DataAccess;
    using Geography.DataAccess.Repository;
    using System.Collections.Generic;
    using Geography.Business.Country.Models;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using AutoMapper;
    using EnsureThat;
    using System.Linq;
    using Framework.Business.Manager.Query;
    using Framework.Service.Utilities.Criteria;
    using System;
    using Geography.Entity.Entities;
    using Framework.Business.Extension;

    public class CountryQueryManager : CodeQueryManager<GeographyReadOnlyDbContext, Country, CountryReadModel>, ICountryQueryManager
    {
        private readonly ICountryQueryRepository _countryRepository;

        public CountryQueryManager(ICountryQueryRepository countryRepository, ILogger<CountryQueryManager> logger, IMapper mapper)
            : base(countryRepository, logger, mapper)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryReadModel>> GetByName(string countryName)
        {
            FilterCriteria<Country> filterCriteria = new FilterCriteria<Country>
            {
                Predicate = item => item.Name == countryName
            };
            var result = await EntityQueryAsync(filterCriteria).ConfigureAwait(false);
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryReadModel>>(result);
        }

        public async Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(string isoCode, params string[] isoCodes)
        {
            EnsureArg.IsNotNullOrEmpty(isoCode, nameof(isoCode));
            return await GetByIsoCodeAsync(isoCodes.Prepend(isoCode)).ConfigureAwait(false);
        }                 

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

        protected override Task<IEnumerable<Country>> EntityQueryAsync(FilterCriteria<Country> filterCriteria)
        {
            filterCriteria.Includes.Add(item => item.States);            
            return base.EntityQueryAsync(filterCriteria);
        }                 
    }
}
