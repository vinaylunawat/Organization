
namespace Framework.Business.Manager.Query
{
    using System.Collections.Generic;
    using System.Threading.Tasks;    

    public interface IQueryManager<TReadModel> : IManagerBase
        where TReadModel : class
    {
        Task<IEnumerable<TReadModel>> GetByIdAsync(long id, params long[] ids);

        Task<IEnumerable<TReadModel>> GetByIdAsync(IEnumerable<long> ids);

        Task<IEnumerable<TReadModel>> GetAllAsync();
    }
}
