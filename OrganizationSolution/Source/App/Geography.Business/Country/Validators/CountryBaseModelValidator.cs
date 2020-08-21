namespace Geography.Business.Country.Validators
{
    using FluentValidation;
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Constant;
    using Geography.Business.Country.Models;

    /// <summary>
    /// Defines the <see cref="CountryBaseModelValidator{TCountryCreateModel}" />.
    /// </summary>
    /// <typeparam name="TCountryCreateModel">.</typeparam>
    public abstract class CountryBaseModelValidator<TCountryCreateModel> : ModelValidator<TCountryCreateModel>
    where TCountryCreateModel : CountryCreateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryBaseModelValidator{TCountryCreateModel}"/> class.
        /// </summary>
        public CountryBaseModelValidator()
        {
            RuleFor(x => x.Code)
                .CodeValidation(CountryErrorCode.CodeRequired, CountryErrorCode.CodeTooLong);

            RuleFor(x => x.Name)
                .NameValidation(CountryErrorCode.NameRequired, CountryErrorCode.NameTooLong);

            RuleFor(x => x.IsoCode)
                .NotEmpty().WithErrorEnum(CountryErrorCode.IsoCodeRequired)
                .MaximumLength(BaseConstants.DataLengths.Code).WithErrorEnum(CountryErrorCode.IsoCodeTooLong);
        }
    }
}
