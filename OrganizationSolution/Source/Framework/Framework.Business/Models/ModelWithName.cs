namespace Framework.Business.Models.Models
{
    public abstract class ModelWithName : Model, IModelWithName
    {
        protected ModelWithName()
        {
        }

        protected ModelWithName(string name)
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
