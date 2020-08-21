namespace Framework.Business.Manager.Command
{
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICommandManager{TErrorCode, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public interface ICommandManager<TErrorCode, TCreateModel, TUpdateModel>
         : IManagerBase
         where TErrorCode : struct, Enum
         where TCreateModel : class
         where TUpdateModel : class, TCreateModel, IModelWithId
    {
        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TCreateModel"/>.</param>
        /// <param name="models">The models<see cref="TCreateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateAsync(TCreateModel model, params TCreateModel[] models);

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateAsync(IEnumerable<TCreateModel> models);

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TUpdateModel"/>.</param>
        /// <param name="models">The models<see cref="TUpdateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> UpdateAsync(TUpdateModel model, params TUpdateModel[] models);

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> UpdateAsync(IEnumerable<TUpdateModel> models);

        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="long[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long id, params long[] ids);

        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(IEnumerable<long> ids);
    }
}
