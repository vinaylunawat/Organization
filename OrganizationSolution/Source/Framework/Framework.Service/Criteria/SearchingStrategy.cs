namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;

    /// <summary>
    /// Defines the <see cref="SearchingStrategy" />.
    /// </summary>
    public class SearchingStrategy : ISearchingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchingStrategy"/> class.
        /// </summary>
        public SearchingStrategy()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchingStrategy"/> class.
        /// </summary>
        /// <param name="searchText">The searchText<see cref="string"/>.</param>
        /// <param name="searchColumnNames">The searchColumnNames<see cref="string[]"/>.</param>
        public SearchingStrategy(string searchText, string[] searchColumnNames)
        {
            SearchText = searchText;
            SearchColumnNames = searchColumnNames;
        }

        /// <summary>
        /// Gets or sets the SearchText.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the SearchColumnNames.
        /// </summary>
        public string[] SearchColumnNames { get; set; }
    }
}
