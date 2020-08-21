namespace Framework.Business.Manager.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public abstract class TenantIdCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>
        : BaseCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity>, ITenantIdCommandManager<TErrorCode, TCreateModel, TUpdateModel>
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TErrorCode : struct, Enum
        where TEntity : BaseEntity, IEntityWithId, IEntityWithTenantId
        where TCreateModel : class, IModel
        where TUpdateModel : class, TCreateModel, IModelWithId
    {
        private readonly IGenericQueryRepository<TReadOnlyDbContext, TEntity> _queryRepository;
        private readonly IGenericCommandRepository<TDbContext, TEntity> _commandRepository;

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

        protected TErrorCode IdNotUnique { get; }

        protected IMapper Mapper { get; }

        protected ModelValidator<TCreateModel> CreateModelValidator { get; }

        protected ModelValidator<TUpdateModel> UpdateModelValidator { get; }

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

        protected virtual async Task<ErrorRecords<TErrorCode>> CreateValidationAsync(long tenantId, IList<IIndexedItem<TCreateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(CreateValidationAsync)}");

            return await Task.FromResult(new ErrorRecords<TErrorCode>()).ConfigureAwait(false);
        }

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

        protected virtual Task CreateAfterMapAsync(long tenantId, IList<IIndexedItem<TCreateModel>> models, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        protected virtual Task UpdateAfterMapAsync(long tenantId, IList<IIndexedItem<TUpdateModel>> models, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }
    }
}
