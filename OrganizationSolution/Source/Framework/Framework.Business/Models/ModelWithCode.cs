namespace Framework.Business.Models.Models
{
    /// <summary>
    /// Defines the <see cref="ModelWithCode" />.
    /// </summary>
    public abstract class ModelWithCode : Model, IModelWithCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithCode"/> class.
        /// </summary>
        protected ModelWithCode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithCode"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        protected ModelWithCode(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public string Code { get; set; }
    }
}
