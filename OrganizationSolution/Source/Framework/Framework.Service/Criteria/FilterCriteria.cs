namespace Framework.Service.Utilities.Criteria
{
    using Framework.Service.Criteria.Abstraction;
    using Framework.Service.Paging.Abstraction;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class FilterCriteria<T>
        where T : class
    {
        public Expression<Func<T, bool>> Predicate { get; set; }

        public IPagingStrategy Paging { get; set; }

        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public IOrderingStrategy Sort { get; set; }
    }
}
