namespace Framework.Service.Extension
{
    using Framework.Entity;
    using Framework.Service.Enumeration;
    using Framework.Service.Paging;
    using Framework.Service.Paging.Abstraction;
    using Framework.Service.Utilities.Criteria;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="QueryableExtensions" />.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// The ToPaginatedListAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="source">The source<see cref="IQueryable{T}"/>.</param>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <param name="pageSize">The pageSize<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IPaginatedList{T}}"/>.</returns>
        public static async Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var count = await source.CountAsync().ConfigureAwait(false);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// The AddPredicate.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="query">The query<see cref="IQueryable{T}"/>.</param>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{T}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> AddPredicate<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {
            if (!(criteria is null) && criteria.Predicate != null)
            {
                query = query.Where(criteria.Predicate);
            }

            return query;
        }

        /// <summary>
        /// The AddIncludes.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="query">The query<see cref="IQueryable{T}"/>.</param>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{T}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> AddIncludes<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {

            if (!(criteria is null) && !(criteria.Includes is null))
            {
                query = criteria.Includes.Aggregate(query, (current, item) => EvaluateInclude(current, item));
            }

            return query;
        }

        /// <summary>
        /// The EvaluateInclude.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="current">The current<see cref="IQueryable{T}"/>.</param>
        /// <param name="item">The item<see cref="Expression{Func{T, object}}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        private static IQueryable<T> EvaluateInclude<T>(IQueryable<T> current, Expression<Func<T, object>> item)
            where T : BaseEntity
        {
            if (item.Body is MethodCallExpression)
            {
                var arguments = ((MethodCallExpression)item.Body).Arguments;
                if (arguments.Count > 1)
                {
                    var navigationPath = string.Empty;
                    for (var rowNumber = 0; rowNumber < arguments.Count; rowNumber++)
                    {
                        string path = string.Empty;
                        var arg = arguments[rowNumber];
                        if (arg.NodeType == ExpressionType.Lambda && ((arg as LambdaExpression).Body.NodeType == ExpressionType.Call))
                        {
                            var innerArgument = ((arg as LambdaExpression).Body as MethodCallExpression).Arguments;
                            for (var innerRowNumber = 0; innerRowNumber < innerArgument.Count; innerRowNumber++)
                            {
                                var innerArgs = innerArgument[innerRowNumber];
                                path += (innerRowNumber > 0 ? "." : string.Empty) + innerArgs.ToString().Substring(innerArgs.ToString().IndexOf('.') + 1);
                            }
                        }
                        else
                        {
                            path = arg.ToString().Substring(arg.ToString().IndexOf('.') + 1);
                        }

                        navigationPath += (rowNumber > 0 ? "." : string.Empty) + path;
                    }

                    return current.Include(navigationPath);
                }
            }

            return current.Include(item);
        }

        /// <summary>
        /// The AddPaging.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="query">The query<see cref="IQueryable{T}"/>.</param>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{T}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> AddPaging<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {
            if (!(criteria is null) && !(criteria.Paging is null))
            {
                var skipCount = (criteria.Paging.PageNumber - 1) * criteria.Paging.PageSize;
                query = query.Skip(skipCount).Take(criteria.Paging.PageSize);
            }

            return query;
        }

        /// <summary>
        /// The AddSorting.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="query">The query<see cref="IQueryable{T}"/>.</param>
        /// <param name="criteria">The criteria<see cref="FilterCriteria{T}"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> AddSorting<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {
            if (!(criteria is null) && criteria.Sort != null && !string.IsNullOrEmpty(criteria.Sort.OrderBy))
            {
                query = query.GetQueryableSort(criteria.Sort.OrderBy, criteria.Sort.Direction);
            }

            return query;
        }

        /// <summary>
        /// The GetQueryableSort.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="query">The query<see cref="IQueryable{T}"/>.</param>
        /// <param name="sortField">The sortField<see cref="string"/>.</param>
        /// <param name="sortDirection">The sortDirection<see cref="SortDirection"/>.</param>
        /// <returns>The <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> GetQueryableSort<T>(this IQueryable<T> query, string sortField, SortDirection sortDirection)
        {
            var propertyNames = sortField.Split(".");
            ParameterExpression pe = Expression.Parameter(typeof(T), string.Empty);
            Expression property = pe;
            foreach (var prop in propertyNames)
            {
                property = Expression.PropertyOrField(property, prop);
            }

            LambdaExpression lambda = Expression.Lambda(property, pe);

            string orderbydir;
            if (sortDirection == SortDirection.Ascending)
                orderbydir = "OrderBy";
            else
                orderbydir = "OrderByDescending";

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                orderbydir,
                new Type[] { typeof(T), property.Type },
                query.Expression,
                Expression.Quote(lambda));

            var returnquery = (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(call);
            return returnquery;
        }

        /// <summary>
        /// The OrderEntitiesByModelsOrder.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TEntity">.</typeparam>
        /// <typeparam name="TReturnType">.</typeparam>
        /// <param name="entities">The entities<see cref="IQueryable{TEntity}"/>.</param>
        /// <param name="models">The models<see cref="IEnumerable{TModel}"/>.</param>
        /// <param name="entityKey">The entityKey<see cref="Func{TEntity, object}"/>.</param>
        /// <param name="entityValue">The entityValue<see cref="Func{TEntity, TReturnType}"/>.</param>
        /// <param name="modelKey">The modelKey<see cref="Func{TModel, object}"/>.</param>
        /// <returns>The <see cref="List{TReturnType}"/>.</returns>
        public static List<TReturnType> OrderEntitiesByModelsOrder<TModel, TEntity, TReturnType>(this IQueryable<TEntity> entities, IEnumerable<TModel> models, Func<TEntity, object> entityKey, Func<TEntity, TReturnType> entityValue, Func<TModel, object> modelKey)
        {
            var returnedModels = new List<TReturnType>();
            var entityDictionary = entities.ToDictionary(e => entityKey(e), e => entityValue(e));

            foreach (TModel model in models)
            {
                TReturnType entity = entityDictionary[modelKey(model)];
                returnedModels.Add(entity);
            }

            return returnedModels;
        }
    }
}
