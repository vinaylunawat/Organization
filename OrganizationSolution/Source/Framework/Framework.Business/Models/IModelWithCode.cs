namespace Framework.Business.Models
{
    /// <summary>
    /// Defines the <see cref="IModelWithCode" />.
    /// </summary>
    public interface IModelWithCode : IModel
    {
        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        string Code { get; set; }
    }
}
