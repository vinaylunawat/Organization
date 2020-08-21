namespace Geography.Business.Business.State.Validators
{
    using FluentValidation;
    using Framework.Business;
    using Framework.Business.Extension;
    using Geography.Business.Business.State.Models;

    /// <summary>
    /// Defines the <see cref="StateBaseModelValidator{TStateCreateModel}" />.
    /// </summary>
    /// <typeparam name="TStateCreateModel">.</typeparam>
    public abstract class StateBaseModelValidator<TStateCreateModel> : ModelValidator<TStateCreateModel>
    where TStateCreateModel : StateCreateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateBaseModelValidator{TStateCreateModel}"/> class.
        /// </summary>
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
