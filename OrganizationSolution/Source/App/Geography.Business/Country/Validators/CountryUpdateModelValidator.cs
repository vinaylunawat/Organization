namespace Geography.Business.Country.Validators
{
    using Framework.Business.Extension;
    using Geography.Business.Country.Models;
    
    public class CountryUpdateModelValidator : CountryBaseModelValidator<CountryUpdateModel>
    {
        public CountryUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .IdValidation(CountryErrorCode.IdMustBeGreaterThanZero);
        }
    }
}
