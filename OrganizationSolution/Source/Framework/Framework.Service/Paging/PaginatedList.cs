namespace Framework.Service.Paging
{
    using Framework.Service.Paging.Abstraction;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Paginated generic list of items.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        public PaginatedList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">Items of the list.</param>
        /// <param name="count">Total number of records.</param>
        /// <param name="pageNumber">Index of current page.</param>
        /// <param name="pageSize">Number of records in each page.</param>
        public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        /// <inheritdoc/>
        public int PageNumber { get; }

        /// <inheritdoc/>
        public int PageSize { get; }

        /// <inheritdoc/>
        public int TotalRecords { get; }

        /// <inheritdoc/>
        public int TotalPages { get; }

        /// <inheritdoc/>
        public int PreviousPage
        {
            get
            {
                return PageNumber > 1 ? PageNumber - 1 : 1;
            }
        }

        /// <inheritdoc/>
        public int NextPage
        {
            get
            {
                return PageNumber < TotalPages ? PageNumber + 1 : TotalPages;
            }
        }
    }
}
