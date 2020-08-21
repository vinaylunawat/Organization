namespace Framework.Business.Models.Models
{
    public abstract class ModelWithCodeName : ModelWithCode, IModelWithName
    {
        protected ModelWithCodeName()
        {
        }

        protected ModelWithCodeName(string code, string name)
            : base(code)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
