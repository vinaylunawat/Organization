namespace Framework.Service.Criteria.Abstraction
{
    using Framework.Service.Enumeration;

    /// <summary>
    /// Defines the <see cref="IOrderingStrategy" />.
    /// </summary>
    public interface IOrderingStrategy
    {
        /// <summary>
        /// Gets or sets the OrderBy.
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the Direction.
        /// </summary>
        SortDirection Direction { get; set; }
    }
}
