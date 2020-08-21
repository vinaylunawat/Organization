namespace Geography.Business.Country.Models
{
    using Framework.Business.Models;

    public class CountryReadModel : CountryUpdateModel, IModelWithId
    {
        public CountryReadModel()
        {
        }

        public CountryReadModel(long id, string code, string name, string isoCode)
            : base(id, code, name, isoCode)
        {
        }
    }
}
