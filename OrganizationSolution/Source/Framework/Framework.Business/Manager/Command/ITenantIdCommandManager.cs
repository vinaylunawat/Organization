namespace Framework.Business.Manager.Command
{
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITenantIdCommandManager<TErrorCode, TCreateModel, TUpdateModel>
        : IManagerBase
        where TErrorCode : struct, Enum
        where TCreateModel : class
        where TUpdateModel : class, TCreateModel, IModelWithId
    {
        Task<ManagerResponse<TErrorCode>> CreateAsync(long tenantId, TCreateModel model, params TCreateModel[] models);

        Task<ManagerResponse<TErrorCode>> CreateAsync(long tenantId, IEnumerable<TCreateModel> models);

        Task<ManagerResponse<TErrorCode>> UpdateAsync(long tenantId, TUpdateModel model, params TUpdateModel[] models);

        Task<ManagerResponse<TErrorCode>> UpdateAsync(long tenantId, IEnumerable<TUpdateModel> models);

        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long tenantId, long id, params long[] ids);

        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long tenantId, IEnumerable<long> ids);
    }
}
