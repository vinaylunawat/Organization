namespace Framework.Service.Extension
{
    using LinqKit;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the <see cref="QuerySearchHelper" />.
    /// </summary>
    public static class QuerySearchHelper
    {
        /// <summary>
        /// The Filtering.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="searchText">The searchText<see cref="string"/>.</param>
        /// <param name="searchColumnNames">The searchColumnNames<see cref="string[]"/>.</param>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Expression{Func{TEntity, bool}}"/>.</returns>
        public static Expression<Func<TEntity, bool>> Filtering<TEntity>(string searchText, string[] searchColumnNames, IEnumerable<long> tenantIds = null)
        {
            var predicate = PredicateBuilder.New<TEntity>(true);
            if (searchColumnNames?.Length >= 0)
            {
                foreach (var columns in searchColumnNames)
                {
                    predicate = predicate.Or(ContainsPredicate<TEntity>(columns, searchText));
                }
            }

            //if (tenantIds != null)
            //{
            //    predicate = predicate.And(item => tenantIds.Contains(item.));
            //}


            return predicate;
        }

        /// <summary>
        /// The ContainsPredicate.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <param name="memberName">The memberName<see cref="string"/>.</param>
        /// <param name="searchValue">The searchValue<see cref="string"/>.</param>
        /// <returns>The <see cref="Expression{Func{TEntity, bool}}"/>.</returns>
        public static Expression<Func<TEntity, bool>> ContainsPredicate<TEntity>(string memberName, string searchValue)
        {
            var parameter = Expression.Parameter(typeof(TEntity), nameof(TEntity));
            Expression body = parameter;
            foreach (var member1 in memberName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member1);
            }

            var expression = Expression.Call(
                body,
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(searchValue)
            );
            return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
        }
    }
}
