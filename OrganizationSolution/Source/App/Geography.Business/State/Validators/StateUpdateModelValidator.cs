namespace Geography.Business.Business.State.Validators
{
    using Framework.Business.Extension;
    using Geography.Business.Business.State.Models;

    public class StateUpdateModelValidator : StateBaseModelValidator<StateUpdateModel>
    {
        public StateUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .IdValidation(StateErrorCode.IdMustBeGreaterThanZero);
        }
    }
}
