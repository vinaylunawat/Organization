namespace Framework.Service.Paging.Abstraction
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IPaginatedList{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IPaginatedList<T> : IPaginatedList, IList<T>
    {
    }
}
