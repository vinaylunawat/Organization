namespace Framework.Business
{
    using Framework.Business.Models;
    using FluentValidation;

    public class ModelValidator<TModel> : AbstractValidator<TModel>
        where TModel : IModel
    {
        public ModelValidator()
        {
        }
    }
}
