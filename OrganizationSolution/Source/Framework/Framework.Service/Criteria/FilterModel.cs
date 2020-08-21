namespace Framework.Service.Criteria
{
    using Framework.Service.Paging;

    /// <summary>
    /// Defines the <see cref="FilterModel" />.
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Gets or sets the SearchingStrategy.
        /// </summary>
        public SearchingStrategy SearchingStrategy { get; set; }

        /// <summary>
        /// Gets or sets the OrderingInfo.
        /// </summary>
        public OrderingStrategy OrderingInfo { get; set; }

        /// <summary>
        /// Gets or sets the PagingInfo.
        /// </summary>
        public PagingStrategy PagingInfo { get; set; }
    }
}
