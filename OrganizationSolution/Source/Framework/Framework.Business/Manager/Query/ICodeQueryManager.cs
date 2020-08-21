namespace Framework.Business.Manager.Query
{
    using Framework.Business.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICodeQueryManager{TReadModel}" />.
    /// </summary>
    /// <typeparam name="TReadModel">.</typeparam>
    public interface ICodeQueryManager<TReadModel> : IQueryManager<TReadModel>
        where TReadModel : class, IModelWithCode
    {
        /// <summary>
        /// The GetByCodeAsync.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="codes">The codes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        Task<IEnumerable<TReadModel>> GetByCodeAsync(string code, params string[] codes);

        /// <summary>
        /// The GetByCodeAsync.
        /// </summary>
        /// <param name="codes">The codes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TReadModel}}"/>.</returns>
        Task<IEnumerable<TReadModel>> GetByCodeAsync(IEnumerable<string> codes);
    }
}
