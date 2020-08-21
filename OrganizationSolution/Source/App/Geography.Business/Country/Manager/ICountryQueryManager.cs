namespace Geography.Business.Country.Manager
{
    using Geography.Business.Country.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Framework.Business.Manager.Query;

    public interface ICountryQueryManager: ICodeQueryManager<CountryReadModel>
    {
        Task<IEnumerable<CountryReadModel>> GetByName(string countryName);

        Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(string isoCode, params string[] isoCodes);

       Task<IEnumerable<CountryReadModel>> GetByIsoCodeAsync(IEnumerable<string> isoCodes);
    }
}
