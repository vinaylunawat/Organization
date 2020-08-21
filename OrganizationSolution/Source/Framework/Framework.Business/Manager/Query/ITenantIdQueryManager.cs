namespace Framework.Business.Manager.Query
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ITenantIdQueryManager{TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadModel">.</typeparam>
    public interface ITenantIdQueryManager<TReadModel> : IManagerBase
        where TReadModel : class
    {
        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="long[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> tenantIds, long id, params long[] ids);

        /// <summary>
        /// The GetByIdAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> tenantIds, IEnumerable<long> ids);

        /// <summary>
        /// The GetAllAsync.
        /// </summary>
        /// <param name="tenantIds">The tenantIds<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        Task<IEnumerable<TReadModel>> GetAllAsync(IEnumerable<long> tenantIds);
    }
}
