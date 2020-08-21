namespace Framework.Business.Extension
{
    using FluentValidation;
    using Framework.Business.Models;
    using Framework.Constant;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="RuleBuilderExtensions" />.
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// The IdValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, long}"/>.</param>
        /// <param name="idMustBeGreaterThanZero">The idMustBeGreaterThanZero<see cref="Enum"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, long}"/>.</returns>
        public static IRuleBuilderOptions<TModel, long> IdValidation<TModel>(this IRuleBuilder<TModel, long> ruleBuilder, Enum idMustBeGreaterThanZero)
            where TModel : class, IModelWithId
        {
            return ruleBuilder
                .GreaterThan(0).WithErrorEnum(idMustBeGreaterThanZero);
        }

        /// <summary>
        /// The UrlValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="urlNotWellFormed">The urlNotWellFormed<see cref="Enum"/>.</param>
        /// <param name="uriKind">The uriKind<see cref="UriKind"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> UrlValidation<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum urlNotWellFormed, UriKind uriKind = UriKind.Absolute)
            where TModel : class
        {
            return ruleBuilder
                .Must(x => Uri.IsWellFormedUriString(x, uriKind)).WithMessage("The format of '{PropertyName}' must be a well formed URL.").WithErrorEnum(urlNotWellFormed);
        }

        /// <summary>
        /// The NameValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="nameRequired">The nameRequired<see cref="Enum"/>.</param>
        /// <param name="nameTooLong">The nameTooLong<see cref="Enum"/>.</param>
        /// <param name="maximumLength">The maximumLength<see cref="int"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> NameValidation<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum nameRequired, Enum nameTooLong, int maximumLength = BaseConstants.DataLengths.Name)
            where TModel : class, IModelWithName, IModel
        {
            return ruleBuilder
                .NotEmpty().WithErrorEnum(nameRequired)
                .MaximumLength(maximumLength).WithErrorEnum(nameTooLong);
        }

        /// <summary>
        /// The CodeValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="codeRequired">The codeRequired<see cref="Enum"/>.</param>
        /// <param name="codeTooLong">The codeTooLong<see cref="Enum"/>.</param>
        /// <param name="maximumLength">The maximumLength<see cref="int"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> CodeValidation<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum codeRequired, Enum codeTooLong, int maximumLength = BaseConstants.DataLengths.Code)
            where TModel : class, IModelWithCode
        {
            return ruleBuilder
                .NotEmpty().WithErrorEnum(codeRequired)
                .MaximumLength(maximumLength).WithErrorEnum(codeTooLong);
        }

        /// <summary>
        /// The CustomStringLengthValidationWithRquired.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="nameRequired">The nameRequired<see cref="Enum"/>.</param>
        /// <param name="nameTooLong">The nameTooLong<see cref="Enum"/>.</param>
        /// <param name="maximumLength">The maximumLength<see cref="int"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> CustomStringLengthValidationWithRquired<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum nameRequired, Enum nameTooLong, int maximumLength = BaseConstants.DataLengths.Name)
           where TModel : class, IModel
        {
            return ruleBuilder
                .NotEmpty().WithErrorEnum(nameRequired)
                .MaximumLength(maximumLength).WithErrorEnum(nameTooLong);
        }

        /// <summary>
        /// The CustomStringLengthValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="nameTooLong">The nameTooLong<see cref="Enum"/>.</param>
        /// <param name="maximumLength">The maximumLength<see cref="int"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> CustomStringLengthValidation<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum nameTooLong, int maximumLength = BaseConstants.DataLengths.Name)
           where TModel : class, IModel
        {
            return ruleBuilder
                .MaximumLength(maximumLength).WithErrorEnum(nameTooLong);
        }

        /// <summary>
        /// The EmailValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{TModel, string}"/>.</param>
        /// <param name="emailAddressIncorrect">The emailAddressIncorrect<see cref="Enum"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{TModel, string}"/>.</returns>
        public static IRuleBuilderOptions<TModel, string> EmailValidation<TModel>(this IRuleBuilder<TModel, string> ruleBuilder, Enum emailAddressIncorrect)
           where TModel : class
        {
            return ruleBuilder
                .Must(x => ValidateEmail(x)).WithMessage(RuleBuilderConstants.EmailValidationMessage).WithErrorEnum(emailAddressIncorrect);
        }

        /// <summary>
        /// The ValidateEmail.
        /// </summary>
        /// <param name="emailAddress">The emailAddress<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool ValidateEmail(string emailAddress)
        {
            var emailValidationRegex = new Regex(RuleBuilderConstants.EmailValidationRegex);
            return emailValidationRegex.Match(emailAddress).Success;
        }
    }
}
