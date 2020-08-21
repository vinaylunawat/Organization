namespace Framework.Service.Paging.Abstraction
{
    /// <summary>
    /// The paging strategy to use when fetching and returning data.
    /// </summary>
    public interface IPagingStrategy
    {
        /// <summary>
        /// Gets or sets the amount of records in the page..
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page index being retrieved..
        /// </summary>
        int PageNumber { get; set; }
    }
}
