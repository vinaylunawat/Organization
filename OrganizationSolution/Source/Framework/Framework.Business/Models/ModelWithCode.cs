namespace Framework.Business.Models.Models
{
    public abstract class ModelWithCode : Model, IModelWithCode
    {
        protected ModelWithCode()
        {
        }

        protected ModelWithCode(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
    }
}
