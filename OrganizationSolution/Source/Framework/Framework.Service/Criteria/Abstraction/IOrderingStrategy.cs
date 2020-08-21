namespace Framework.Service.Criteria.Abstraction
{
    using Framework.Service.Enumeration;

    public interface IOrderingStrategy
    {
        string OrderBy { get; set; }

        SortDirection Direction { get; set; }
    }
}
