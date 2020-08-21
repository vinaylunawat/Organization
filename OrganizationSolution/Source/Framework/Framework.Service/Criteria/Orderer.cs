namespace Framework.Service.Criteria
{
    using Framework.Service.Criteria.Abstraction;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Sorting by expression.
    /// </summary>
    /// <typeparam name="T">Entity on which Sorting will apply.</typeparam>
    /// <typeparam name="TKey">Key on the basis of which sorting will work.</typeparam>
    public class Orderer<T, TKey> : IOrderer<T>
    {
        /// <summary>
        /// Defines the _orderExpression.
        /// </summary>
        private readonly Expression<Func<T, TKey>> _orderExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Orderer{T, TKey}"/> class.
        /// </summary>
        /// <param name="orderExpression">The expression used for ordering the list of entities.</param>
        public Orderer(Expression<Func<T, TKey>> orderExpression)
        {
            _orderExpression = orderExpression;
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> ApplyOrderBy(IQueryable<T> source)
        {
            return source.OrderBy(_orderExpression);
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> ApplyOrderByDescending(IQueryable<T> source)
        {
            return source.OrderByDescending(_orderExpression);
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> ApplyThenBy(IOrderedQueryable<T> source)
        {
            return source.ThenBy(_orderExpression);
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> ApplyThenByDescending(IOrderedQueryable<T> source)
        {
            return source.ThenByDescending(_orderExpression);
        }
    }
}
