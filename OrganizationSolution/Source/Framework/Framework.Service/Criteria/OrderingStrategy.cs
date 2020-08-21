namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;
    using Framework.Service.Enumeration;

    /// <summary>
    /// Defines the <see cref="OrderingStrategy" />.
    /// </summary>
    public class OrderingStrategy : IOrderingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingStrategy"/> class.
        /// </summary>
        public OrderingStrategy()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingStrategy"/> class.
        /// </summary>
        /// <param name="orderBy">The orderBy<see cref="string"/>.</param>
        /// <param name="direction">The direction<see cref="SortDirection"/>.</param>
        public OrderingStrategy(string orderBy, SortDirection direction)
        {
            OrderBy = orderBy;
            Direction = direction;
        }

        /// <summary>
        /// Gets or sets the OrderBy.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the Direction.
        /// </summary>
        public SortDirection Direction { get; set; }
    }
}
