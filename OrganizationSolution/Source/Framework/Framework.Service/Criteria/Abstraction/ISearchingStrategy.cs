namespace Framework.Service.Criteria.Abstraction
{
    /// <summary>
    /// Defines the <see cref="ISearchingStrategy" />.
    /// </summary>
    public interface ISearchingStrategy
    {
        /// <summary>
        /// Gets or sets the SearchText.
        /// </summary>
        string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the SearchColumnNames.
        /// </summary>
        string[] SearchColumnNames { get; set; }
    }
}
