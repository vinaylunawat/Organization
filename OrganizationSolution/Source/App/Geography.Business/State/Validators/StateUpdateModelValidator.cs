namespace Geography.Business.Business.State.Validators
{
    using Framework.Business.Extension;
    using Geography.Business.Business.State.Models;

    /// <summary>
    /// Defines the <see cref="StateUpdateModelValidator" />.
    /// </summary>
    public class StateUpdateModelValidator : StateBaseModelValidator<StateUpdateModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateUpdateModelValidator"/> class.
        /// </summary>
        public StateUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .IdValidation(StateErrorCode.IdMustBeGreaterThanZero);
        }
    }
}
