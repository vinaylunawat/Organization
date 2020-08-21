namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;
    using Framework.Service.Enumeration;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Sorting class.
    /// </summary>
    /// <typeparam name="T">Entity on which Sorting will apply.</typeparam>
    public class Sort<T>
    {
        /// <summary>
        /// Gets or sets the sort direction to apply (ascending vs descending)..
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the Order
        /// Gets or sets an <see cref="IOrderer{T}"/> used to apply the sorting..
        /// </summary>
        public IOrderer<T> Order { get; set; }

        /// <summary>
        /// Applys an ascending sort based on the key.
        /// </summary>
        /// <typeparam name="TKey">The key to apply the sort by.</typeparam>
        /// <param name="sort">The sorting information to apply.</param>
        /// <returns>An <see cref="Sort{T}"/> with the sorting information.</returns>
        public static Sort<T> Asc<TKey>(Expression<Func<T, TKey>> sort)
        {
            return new Sort<T>
            {
                Direction = SortDirection.Ascending,
                Order = new Orderer<T, TKey>(sort),
            };
        }

        /// <summary>
        /// Applys a descending sort based on the key.
        /// </summary>
        /// <typeparam name="TKey">The key to apply the sort by.</typeparam>
        /// <param name="sort">The sorting information to apply.</param>
        /// <returns>An <see cref="Sort{T}"/> with the sorting information.</returns>
        public static Sort<T> Desc<TKey>(Expression<Func<T, TKey>> sort)
        {
            return new Sort<T>
            {
                Direction = SortDirection.Descending,
                Order = new Orderer<T, TKey>(sort),
            };
        }
    }
}
