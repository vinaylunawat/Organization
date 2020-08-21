namespace Geography.Business.Country.Manager
{
    using Framework.Business;
    using Framework.Business.Manager.Command;
    using Geography.Business.Country.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICountryCommandManager" />.
    /// </summary>
    public interface ICountryCommandManager : ICodeCommandManager<CountryErrorCode, CountryCreateModel, CountryUpdateModel>
    {
        /// <summary>
        /// The DeleteByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCode">The isoCode<see cref="string"/>.</param>
        /// <param name="isoCodes">The isoCodes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{CountryErrorCode}}"/>.</returns>
        Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(string isoCode, params string[] isoCodes);

        /// <summary>
        /// The DeleteByIsoCodeAsync.
        /// </summary>
        /// <param name="isoCodes">The isoCodes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{CountryErrorCode}}"/>.</returns>
        Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(IEnumerable<string> isoCodes);
    }
}
