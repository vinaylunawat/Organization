namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;

    public class SearchingStrategy : ISearchingStrategy
    {
        public SearchingStrategy()
        {
        }

        public SearchingStrategy(string searchText, string[] searchColumnNames)
        {
            SearchText = searchText;
            SearchColumnNames = searchColumnNames;
        }

        public string SearchText { get; set; }

        public string[] SearchColumnNames { get; set; }
    }
}
