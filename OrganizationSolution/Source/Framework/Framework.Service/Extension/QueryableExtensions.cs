namespace Framework.Service.Extension
{
    using Framework.Constant;
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

    public static class QueryableExtensions
    {
        public static async Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var count = await source.CountAsync().ConfigureAwait(false);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
        public static IQueryable<T> AddPredicate<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {
            if (!(criteria is null) && criteria.Predicate != null)
            {
                query = query.Where(criteria.Predicate);
            }

            return query;
        }

        public static IQueryable<T> AddIncludes<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {

            if (!(criteria is null) && !(criteria.Includes is null))
            {
                query = criteria.Includes.Aggregate(query, (current, item) => EvaluateInclude(current, item));
            }

            return query;
        }

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

        public static IQueryable<T> AddSorting<T>(this IQueryable<T> query, FilterCriteria<T> criteria)
            where T : BaseEntity
        {
            if (!(criteria is null) && criteria.Sort != null && !string.IsNullOrEmpty(criteria.Sort.OrderBy))
            {
                query = query.GetQueryableSort(criteria.Sort.OrderBy, criteria.Sort.Direction);
            }

            return query;
        }

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
