namespace Framework.Business.Models
{
    /// <summary>
    /// Defines the <see cref="IModelWithName" />.
    /// </summary>
    public interface IModelWithName : IModel
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        string Name { get; set; }
    }
}
