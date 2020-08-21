namespace Framework.Business.Manager.Command
{
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommandManager<TErrorCode, TCreateModel, TUpdateModel>
         : IManagerBase
         where TErrorCode : struct, Enum
         where TCreateModel : class
         where TUpdateModel : class, TCreateModel, IModelWithId
    {
        Task<ManagerResponse<TErrorCode>> CreateAsync(TCreateModel model, params TCreateModel[] models);

        Task<ManagerResponse<TErrorCode>> CreateAsync(IEnumerable<TCreateModel> models);

        Task<ManagerResponse<TErrorCode>> UpdateAsync(TUpdateModel model, params TUpdateModel[] models);

        Task<ManagerResponse<TErrorCode>> UpdateAsync(IEnumerable<TUpdateModel> models);

        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long id, params long[] ids);

        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(IEnumerable<long> ids);
    }
}
