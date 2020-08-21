namespace Framework.Business.Manager.Command
{
    using AutoMapper;
    using EnsureThat;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TenantIdCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public abstract class TenantIdCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>
        : BaseCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity>, ITenantIdCommandManager<TErrorCode, TCreateModel, TUpdateModel>
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TErrorCode : struct, Enum
        where TEntity : BaseEntity, IEntityWithId, IEntityWithTenantId
        where TCreateModel : class, IModel
        where TUpdateModel : class, TCreateModel, IModelWithId
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
        /// Initializes a new instance of the <see cref="TenantIdCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}"/> class.
        /// </summary>
        /// <param name="queryRepository">The queryRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="commandRepository">The commandRepository<see cref="IGenericCommandRepository{TDbContext, TEntity}"/>.</param>
        /// <param name="createModelValidator">The createModelValidator<see cref="ModelValidator{TCreateModel}"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="ModelValidator{TUpdateModel}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{TenantIdCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="idDoesNotExist">The idDoesNotExist<see cref="TErrorCode"/>.</param>
        /// <param name="idNotUnique">The idNotUnique<see cref="TErrorCode"/>.</param>
        protected TenantIdCommandManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryRepository,
            IGenericCommandRepository<TDbContext, TEntity> commandRepository, ModelValidator<TCreateModel> createModelValidator, ModelValidator<TUpdateModel> updateModelValidator, ILogger<TenantIdCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>> logger, IMapper mapper, TErrorCode idDoesNotExist, TErrorCode idNotUnique)
            : base(queryRepository, commandRepository, logger, idDoesNotExist)
        {
            EnsureArg.IsNotNull(createModelValidator, nameof(createModelValidator));
            EnsureArg.IsNotNull(updateModelValidator, nameof(updateModelValidator));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            CreateModelValidator = createModelValidator;
            UpdateModelValidator = updateModelValidator;
            Mapper = mapper;
            IdNotUnique = idNotUnique;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        /// Gets the IdNotUnique.
        /// </summary>
        protected TErrorCode IdNotUnique { get; }

        /// <summary>
        /// Gets the Mapper.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Gets the CreateModelValidator.
        /// </summary>
        protected ModelValidator<TCreateModel> CreateModelValidator { get; }

        /// <summary>
        /// Gets the UpdateModelValidator.
        /// </summary>
        protected ModelValidator<TUpdateModel> UpdateModelValidator { get; }

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="model">The model<see cref="TCreateModel"/>.</param>
        /// <param name="models">The models<see cref="TCreateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> CreateAsync(long tenantId, TCreateModel model, params TCreateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await CreateAsync(tenantId, models.Prepend(model)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> CreateAsync(long tenantId, IEnumerable<TCreateModel> models)
        {
            try
            {
                EnsureArg.IsGt(tenantId, 0, nameof(tenantId));
                EnsureArg.IsNotNull(models, nameof(models));
                EnsureArgExtensions.HasItems(models, nameof(models));

                var indexedModels = models.ToIndexedItems().ToArray();
                var errorRecords = CreateModelValidator.ExecuteCreateValidation<TErrorCode, TCreateModel>(indexedModels);
                var customErrorRecords = await CreateValidationAsync(tenantId, indexedModels).ConfigureAwait(false);
                var mergedErrorRecords = errorRecords.Merge(customErrorRecords);

                if (mergedErrorRecords.Any())
                {
                    return new ManagerResponse<TErrorCode>(mergedErrorRecords);
                }
                else
                {
                    var entities = new List<TEntity>();

                    Mapper.Map(models, entities);
                    await CreateAfterMapAsync(tenantId, indexedModels, entities).ConfigureAwait(false);

                    var ordinalPosition = 0;
                    foreach (var entity in entities)
                    {
                        entity.TenantId = tenantId;
                        ordinalPosition++;
                    }

                    await _commandRepository.Insert(entities).ConfigureAwait(false);
                    return new ManagerResponse<TErrorCode>(entities.Select(x => x.Id));
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="model">The model<see cref="TUpdateModel"/>.</param>
        /// <param name="models">The models<see cref="TUpdateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> UpdateAsync(long tenantId, TUpdateModel model, params TUpdateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await UpdateAsync(tenantId, models.Prepend(model).ToArray()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(UpdateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The UpdateAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> UpdateAsync(long tenantId, IEnumerable<TUpdateModel> models)
        {
            try
            {
                EnsureArg.IsNotNull(models, nameof(models));
                EnsureArgExtensions.HasItems(models, nameof(models));

                var indexedModels = models.ToIndexedItems().ToList();
                var errorRecords = UpdateModelValidator.ExecuteUpdateValidation<TErrorCode, TUpdateModel>(indexedModels);
                var customErrorRecords = await UpdateValidationAsync(tenantId, indexedModels).ConfigureAwait(false);

                var mergedErrorRecords = errorRecords.Merge(customErrorRecords);

                if (mergedErrorRecords.Any())
                {
                    return new ManagerResponse<TErrorCode>(mergedErrorRecords);
                }
                else
                {
                    var entities = new List<TEntity>();

                    Mapper.Map(models, entities);
                    await UpdateAfterMapAsync(tenantId, indexedModels, entities).ConfigureAwait(false);
                    await _commandRepository.Update(entities).ConfigureAwait(false);
                    return new ManagerResponse<TErrorCode>(entities.Select(x => x.Id));
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(UpdateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="id">The id<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="long[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long tenantId, long id, params long[] ids)
        {
            try
            {
                EnsureArg.IsGt(id, 0, nameof(id));

                return await DeleteByIdAsync(tenantId, ids.Prepend(id)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIdAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The DeleteByIdAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="ids">The ids<see cref="IEnumerable{long}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long tenantId, IEnumerable<long> ids)
        {
            try
            {
                EnsureArg.IsNotNull(ids, nameof(ids));
                EnsureArgExtensions.HasItems(ids, nameof(ids));

                return await DeleteByExpressionAsync(ids, x => ids.Contains(x.Id) && x.TenantId == tenantId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIdAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateValidationAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TCreateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected virtual async Task<ErrorRecords<TErrorCode>> CreateValidationAsync(long tenantId, IList<IIndexedItem<TCreateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(CreateValidationAsync)}");

            return await Task.FromResult(new ErrorRecords<TErrorCode>()).ConfigureAwait(false);
        }

        /// <summary>
        /// The UpdateValidationAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected virtual async Task<ErrorRecords<TErrorCode>> UpdateValidationAsync(long tenantId, IList<IIndexedItem<TUpdateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(UpdateValidationAsync)}");

            var existsValidationError = await ValidationHelpers.ExistsValidationAsync(
                async (keys) =>
                {
                    var modelIds = keys.Select(x => x).ToList();
                    var result = await _queryRepository.FetchByAsync(x => modelIds.Contains(x.Id) && x.TenantId == tenantId, x => new IdKey<long>(x.Id, x.Id)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.Id,
                IdDoesNotExist).ConfigureAwait(false);

            var duplicateErrorIds = ValidationHelpers.DuplicateValidation(indexedModels, item => item.Item.Id, IdNotUnique);
            return new ErrorRecords<TErrorCode>(existsValidationError.Concat(duplicateErrorIds));
        }

        /// <summary>
        /// The CreateAfterMapAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="models">The models<see cref="IList{IIndexedItem{TCreateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task CreateAfterMapAsync(long tenantId, IList<IIndexedItem<TCreateModel>> models, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// The UpdateAfterMapAsync.
        /// </summary>
        /// <param name="tenantId">The tenantId<see cref="long"/>.</param>
        /// <param name="models">The models<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <param name="entities">The entities<see cref="IList{TEntity}"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual Task UpdateAfterMapAsync(long tenantId, IList<IIndexedItem<TUpdateModel>> models, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }
    }
}
