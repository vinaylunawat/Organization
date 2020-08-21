namespace Framework.Service.Criteria.Abstraction
{
    using System.Linq;

    /// <summary>
    /// A Orderer Interface, responsible for sorting final result.
    /// </summary>
    /// <typeparam name="T">Applies on TEntity.</typeparam>
    public interface IOrderer<T>
    {
        /// <summary>
        /// Applies an order by ascending.
        /// </summary>
        /// <param name="source">An <see cref="IQueryable{T}"/> to apply the order by to.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> with the ordered <see cref="IQueryable{T}"/>.</returns>
        IOrderedQueryable<T> ApplyOrderBy(IQueryable<T> source);

        /// <summary>
        /// Applies an order by decending.
        /// </summary>
        /// <param name="source">An <see cref="IQueryable{T}"/> to apply the order by descending to.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> with the ordered <see cref="IQueryable{T}"/>.</returns>
        IOrderedQueryable<T> ApplyOrderByDescending(IQueryable<T> source);

        /// <summary>
        /// Applies an order by ascending after an initial order by has been applied.
        /// </summary>
        /// <param name="source">An <see cref="IOrderedQueryable{T}"/> to apply the order by to.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> with the further ordered <see cref="IOrderedQueryable{T}"/>.</returns>
        IOrderedQueryable<T> ApplyThenBy(IOrderedQueryable<T> source);

        /// <summary>
        /// Applies an order by descending after an initial order by has been applied.
        /// </summary>
        /// <param name="source">An <see cref="IOrderedQueryable{T}"/> to apply the order by descending to.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> with the further ordered <see cref="IOrderedQueryable{T}"/>.</returns>
        IOrderedQueryable<T> ApplyThenByDescending(IOrderedQueryable<T> source);
    }
}
