namespace Framework.DataAccess.Repository
{
    using Framework.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IGenericCommandRepository{TDbContext, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public interface IGenericCommandRepository<TDbContext, TEntity>
       where TDbContext : BaseDbContext<TDbContext>
       where TEntity : BaseEntity
    {
        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> Insert(TEntity entity);

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{TEntity}"/>.</returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(long id);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="TEntity"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        Task<bool> Delete(IEnumerable<TEntity> entity);

        /// <summary>
        /// The UpdateOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="IEnumerable{TEntity}"/>.</returns>
        IEnumerable<TEntity> UpdateOnly(IEnumerable<TEntity> entities);

        /// <summary>
        /// The InsertOnly.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        Task<IEnumerable<TEntity>> InsertOnly(IEnumerable<TEntity> entities);

        /// <summary>
        /// The SaveOnly.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task SaveOnly();
    }
}
