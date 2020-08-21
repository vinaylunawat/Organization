namespace Framework.Service.Paging.Abstraction
{
    /// <summary>
    /// Pagination information.
    /// </summary>
    public interface IPaginatedList
    {
        /// <summary>
        /// Gets the page number..
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Gets the number of records in each page..
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the total number of records..
        /// </summary>
        int TotalRecords { get; }

        /// <summary>
        /// Gets the total number of pages..
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets the previous page index if there is a previous page, otherwise it returns the first page..
        /// </summary>
        int PreviousPage { get; }

        /// <summary>
        /// Gets the next page index if there is a next page, otherwise it returns the last page..
        /// </summary>
        int NextPage { get; }
    }
}
