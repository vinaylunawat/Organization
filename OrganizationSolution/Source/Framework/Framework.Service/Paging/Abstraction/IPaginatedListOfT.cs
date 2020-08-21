namespace Framework.Service.Paging.Abstraction
{
    using System.Collections.Generic;
    public interface IPaginatedList<T> : IPaginatedList, IList<T>
    {
    }
}
