namespace Geography.Business.Business.State.Validators
{
    using Geography.Business.Business.State.Models;
    using Framework.Business;
    using FluentValidation;
    using Framework.Business.Extension;

    public abstract class StateBaseModelValidator<TStateCreateModel> : ModelValidator<TStateCreateModel>
    where TStateCreateModel : StateCreateModel
    {
        public StateBaseModelValidator()
        {
            RuleFor(x => x.Code)
                .CodeValidation(StateErrorCode.CodeRequired, StateErrorCode.CodeTooLong);

            RuleFor(x => x.Name)
                .NameValidation(StateErrorCode.NameRequired, StateErrorCode.NameTooLong);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithErrorEnum(StateErrorCode.CountryCodeRequired);
            
        }
    }

     
}
