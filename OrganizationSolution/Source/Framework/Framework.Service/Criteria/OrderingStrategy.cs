namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;
    using Framework.Service.Enumeration;

    public class OrderingStrategy : IOrderingStrategy
    {
        public OrderingStrategy()
        {

        }
        public OrderingStrategy(string orderBy, SortDirection direction)
        {
            OrderBy = orderBy;
            Direction = direction;
        }

        public string OrderBy { get; set; }

        public SortDirection Direction { get; set; }
    }
}
