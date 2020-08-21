namespace Geography.Business.Country.Validators
{
    using Framework.Business.Extension;
    using Geography.Business.Country.Models;

    /// <summary>
    /// Defines the <see cref="CountryUpdateModelValidator" />.
    /// </summary>
    public class CountryUpdateModelValidator : CountryBaseModelValidator<CountryUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryUpdateModelValidator"/> class.
        /// </summary>
        public CountryUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .IdValidation(CountryErrorCode.IdMustBeGreaterThanZero);
        }
    }
}
