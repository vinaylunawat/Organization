namespace Framework.Business
{
    using FluentValidation;
    using Framework.Business.Models;

    /// <summary>
    /// Defines the <see cref="ModelValidator{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">.</typeparam>
    public class ModelValidator<TModel> : AbstractValidator<TModel>
        where TModel : IModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValidator{TModel}"/> class.
        /// </summary>
        public ModelValidator()
        {
        }
    }
}
