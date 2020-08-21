namespace Framework.Service.Paging
{
    using Framework.Constant;
    using Framework.Service.Paging.Abstraction;

    /// <summary>
    /// Paging Strategy class.
    /// </summary>
    public class PagingStrategy : IPagingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingStrategy"/> class.
        /// </summary>
        public PagingStrategy()
        {
            PageNumber = 1;
            PageSize = PaginationConstants.MaxPageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingStrategy"/> class.
        /// </summary>
        /// <param name="pageNumber">Page Number.</param>
        /// <param name="pageSize">Page Size.</param>
        public PagingStrategy(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the PageSize
        /// Gets or Sets Page Size..
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the PageNumber
        /// Gets or Sets Page Number..
        /// </summary>
        public int PageNumber { get; set; }
    }
}
