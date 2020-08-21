namespace Framework.Business.Manager.Query
{
    using Framework.Business.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICodeQueryManager<TReadModel> : IQueryManager<TReadModel>
        where TReadModel : class, IModelWithCode
    {
        Task<IEnumerable<TReadModel>> GetByCodeAsync(string code, params string[] codes);

        Task<IEnumerable<TReadModel>> GetByCodeAsync(IEnumerable<string> codes);
    }
}
