namespace Geography.DataLoader.DataUnits
{
    using EnsureThat;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Framework.DataLoader;
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Framework.Business.Extension;

    public class CountryDataUnit : DataUnit
    {
        private readonly ICountryCommandManager _countryCommandManager;
        private readonly ICountryQueryManager _countryQueryManager;

        public CountryDataUnit(ICountryCommandManager countryCommandManager, ICountryQueryManager countryQueryManager, ILogger<CountryDataUnit> logger)
            : base(logger)
        {
            EnsureArg.IsNotNull(countryCommandManager, nameof(countryCommandManager));
            EnsureArg.IsNotNull(countryQueryManager, nameof(countryQueryManager));

            _countryCommandManager = countryCommandManager;
            _countryQueryManager = countryQueryManager;
        }

        public async override Task LoadSeedDataAsync()
        {
            try
            {
                var countryModels = new List<CountryCreateModel>()
                {
                    new CountryCreateModel("US","United States of America","US"),
                    new CountryCreateModel( "CA","Canada","CA")
                };

                var existingModels = await _countryQueryManager.GetByIsoCodeAsync(countryModels.Select(x => x.IsoCode)).ConfigureAwait(false);
                var modelsToCreate = countryModels.Where(e => !existingModels.Any(m => m.IsoCode == e.IsoCode));

                if (modelsToCreate.Any())
                {
                    var result = await _countryCommandManager.CreateAsync(modelsToCreate).ConfigureAwait(false);
                    result.ThrowIfError();
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(LoadSeedDataAsync));
                throw;
            }
        }
    }
}
