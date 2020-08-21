namespace Framework.Service.Criteria.Abstraction
{
    public interface ISearchingStrategy
    {
        string SearchText { get; set; }

        string[] SearchColumnNames { get; set; }
    }
}
