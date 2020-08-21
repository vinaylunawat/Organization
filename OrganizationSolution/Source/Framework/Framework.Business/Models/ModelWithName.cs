namespace Framework.Business.Models.Models
{
    /// <summary>
    /// Defines the <see cref="ModelWithName" />.
    /// </summary>
    public abstract class ModelWithName : Model, IModelWithName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithName"/> class.
        /// </summary>
        protected ModelWithName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelWithName"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        protected ModelWithName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }
    }
}
