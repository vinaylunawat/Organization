namespace Geography.Business.Country.Manager
{
    using Framework.Business.Manager.Query;
    using Geography.Business.Country.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICountryQueryManager" />.
    /// </summary>
    public interface ICountryQueryManager : ICodeQueryManager<CountryReadModel>
    {
        /// <summary>
        /// The GetByName.
        /// </summary>
        /// <param name="countryName">The countryName<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        Task<IEnumerable<CountryReadModel>> GetByName(string countryName);

        /// <summary>
        /// The GetByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        /// <param name="isoCodes">The isoCodes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(string isoCode, params string[] isoCodes);

        /// <summary>
        /// The GetByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCodes">The isoCodes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{CountryReadModel}}"/>.</returns>
        Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(IEnumerable<string> isoCodes);
    }
}
