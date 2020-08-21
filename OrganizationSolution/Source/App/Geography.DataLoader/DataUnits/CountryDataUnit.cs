namespace Geography.DataLoader.DataUnits
{
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.DataLoader;
    using Geography.Business.Country.Manager;
    using Geography.Business.Country.Models;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryDataUnit" />.
    /// </summary>
    public class CountryDataUnit : DataUnit
    {
        /// <summary>
        /// Defines the _countryCommandManager.
        /// </summary>
        private readonly ICountryCommandManager _countryCommandManager;

        /// <summary>
        /// Defines the _countryQueryManager.
        /// </summary>
        private readonly ICountryQueryManager _countryQueryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDataUnit"/> class.
        /// </summary>
        /// <param name="countryCommandManager">The countryCommandManager<see cref="ICountryCommandManager"/>.</param>
        /// <param name="countryQueryManager">The countryQueryManager<see cref="ICountryQueryManager"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CountryDataUnit}"/>.</param>
        public CountryDataUnit(ICountryCommandManager countryCommandManager, ICountryQueryManager countryQueryManager, ILogger<CountryDataUnit> logger)
            : base(logger)
        {
            EnsureArg.IsNotNull(countryCommandManager, nameof(countryCommandManager));
            EnsureArg.IsNotNull(countryQueryManager, nameof(countryQueryManager));

            _countryCommandManager = countryCommandManager;
            _countryQueryManager = countryQueryManager;
        }

        /// <summary>
        /// The LoadSeedDataAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
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
