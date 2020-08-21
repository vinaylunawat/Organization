namespace Framework.Business.Models.Models
{
    /// <summary>
    /// Defines the <see cref="ModelWithCodeName" />.
    /// </summary>
    public abstract class ModelWithCodeName : ModelWithCode, IModelWithName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithCodeName"/> class.
        /// </summary>
        protected ModelWithCodeName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithCodeName"/> class.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        protected ModelWithCodeName(string code, string name)
            : base(code)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
