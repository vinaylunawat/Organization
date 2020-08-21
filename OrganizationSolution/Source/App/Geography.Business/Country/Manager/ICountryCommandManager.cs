namespace Geography.Business.Country.Manager
{
    using Geography.Business.Country.Models;
    using Framework.Business;
    using Framework.Business.Manager.Command;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountryCommandManager : ICodeCommandManager<CountryErrorCode, CountryCreateModel, CountryUpdateModel>
    {
        Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(string isoCode, params string[] isoCodes);
        Task<ManagerResponse<CountryErrorCode>> DeleteByIsoCodeAsync(IEnumerable<string> isoCodes);
    }
}
