namespace Framework.Business.Manager.Command
{
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICodeCommandManager{TErrorCode, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public interface ICodeCommandManager<TErrorCode, TCreateModel, TUpdateModel>
        : ICommandManager<TErrorCode, TCreateModel, TUpdateModel>
        where TErrorCode : struct, Enum
        where TCreateModel : class, IModelWithCode
        where TUpdateModel : class, TCreateModel, IModelWithCode, IModelWithId
    {
        /// <summary>
        /// The DeleteByCodeAsync.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="codes">The codes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> DeleteByCodeAsync(string code, params string[] codes);

        /// <summary>
        /// The DeleteByCodeAsync.
        /// </summary>
        /// <param name="codes">The codes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> DeleteByCodeAsync(IEnumerable<string> codes);

        /// <summary>
        /// The CreateIfNotExistByCodeAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TCreateModel"/>.</param>
        /// <param name="models">The models<see cref="TCreateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateIfNotExistByCodeAsync(TCreateModel model, params TCreateModel[] models);

        /// <summary>
        /// The CreateIfNotExistByCodeAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateIfNotExistByCodeAsync(IEnumerable<TCreateModel> models);

        /// <summary>
        /// The CreateOrUpdateByCodeAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TUpdateModel"/>.</param>
        /// <param name="models">The models<see cref="TUpdateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateOrUpdateByCodeAsync(TUpdateModel model, params TUpdateModel[] models);

        /// <summary>
        /// The CreateOrUpdateByCodeAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        Task<ManagerResponse<TErrorCode>> CreateOrUpdateByCodeAsync(IEnumerable<TUpdateModel> models);
    }
}
