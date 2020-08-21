namespace Geography.Business.Country.Validators
{
    using Geography.Business.Country.Models;
    using Framework.Business;
    using Framework.Business.Extension;
    using FluentValidation;
    using Framework.Constant;

    public abstract class CountryBaseModelValidator<TCountryCreateModel> : ModelValidator<TCountryCreateModel>
    where TCountryCreateModel : CountryCreateModel
    {
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
