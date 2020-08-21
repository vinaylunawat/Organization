namespace Framework.Business.Manager.Command
{
    using EnsureThat;
    using Framework.Constant;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="BaseCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    public abstract class BaseCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity> : ManagerBase
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TErrorCode : struct, Enum
        where TEntity : BaseEntity, IEntityWithId
    {
        /// <summary>
        /// Defines the _queryRepository.
        /// </summary>
        private readonly IGenericQueryRepository<TReadOnlyDbContext, TEntity> _queryRepository;

        /// <summary>
        /// Defines the _commandRepository.
        /// </summary>
        private readonly IGenericCommandRepository<TDbContext, TEntity> _commandRepository;

        /// <summary>
        /// Gets the IdDoesNotExist.
        /// </summary>
        protected TErrorCode IdDoesNotExist { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity}"/> class.
        /// </summary>
        /// <param name="queryRepository">The queryRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="commandRepository">The commandRepository<see cref="IGenericCommandRepository{TDbContext, TEntity}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{BaseCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity}}"/>.</param>
        /// <param name="idDoesNotExist">The idDoesNotExist<see cref="TErrorCode"/>.</param>
        protected BaseCommandManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryRepository, IGenericCommandRepository<TDbContext, TEntity> commandRepository, ILogger<BaseCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity>> logger, TErrorCode idDoesNotExist)
            : base(logger)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            IdDoesNotExist = idDoesNotExist;
        }

        /// <summary>
        /// The DeleteByExpressionAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="keys">The keys<see cref="IEnumerable{T}"/>.</param>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        protected virtual async Task<ManagerResponse<TErrorCode>> DeleteByExpressionAsync<T>(IEnumerable<T> keys, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                EnsureArg.IsNotNull(predicate, nameof(predicate));

                var entities = await DeleteEntityQueryAsync(predicate).ConfigureAwait(false);

                var errorRecords = await DeleteValidationAsync(keys, entities).ConfigureAwait(false);

                if (errorRecords.Any())
                {
                    return new ManagerResponse<TErrorCode>(errorRecords);
                }
                else
                {
                    await _commandRepository.Delete(entities).ConfigureAwait(false);
                    return new ManagerResponse<TErrorCode>(entities.Select(x => x.Id));
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByExpressionAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The DeleteValidationAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="keys">The keys<see cref="IEnumerable{T}"/>.</param>
        /// <param name="entities">The entities<see cref="IEnumerable{TEntity}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected virtual async Task<ErrorRecords<TErrorCode>> DeleteValidationAsync<T>(IEnumerable<T> keys, IEnumerable<TEntity> entities)
        {
            var errorRecords = new ErrorRecords<TErrorCode>();
            if (entities.Any())
            {
                var ordinalPosition = 0;
                foreach (var key in keys)
                {
                    var isIdExists = false;
                    if (key is long)
                    {
                        isIdExists = entities.Any(x => x.Id.Equals(key));
                    }

                    if (!isIdExists)
                    {
                        var errorMessage = new ErrorMessage<TErrorCode>(SecurityConstants.Id, IdDoesNotExist, SecurityConstants.IdDoesNotExistValidationMessage, key);
                        var errorRecord = new ErrorRecord<TErrorCode>(ordinalPosition, errorMessage);
                        errorRecords.Add(errorRecord);
                    }
                    ordinalPosition++;
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    var errorMessage = new ErrorMessage<TErrorCode>(SecurityConstants.Id, IdDoesNotExist, SecurityConstants.IdDoesNotExistValidationMessage, key);
                    var errorRecord = new ErrorRecord<TErrorCode>(0, errorMessage);
                    errorRecords.Add(errorRecord);
                }
            }
            return await Task.FromResult(errorRecords);
        }

        /// <summary>
        /// The DeleteEntityQueryAsync.
        /// </summary>
        /// <param name="predicate">The predicate<see cref="Expression{Func{TEntity, bool}}"/>.</param>
        /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/>.</returns>
        protected virtual async Task<IEnumerable<TEntity>> DeleteEntityQueryAsync(Expression<Func<TEntity, bool>> predicate)
        {
            EnsureArg.IsNotNull(predicate, nameof(predicate));
            return await _queryRepository.FetchByAsync(predicate).ConfigureAwait(false);
        }
    }
}
