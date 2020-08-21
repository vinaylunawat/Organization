namespace Framework.Business.Manager.Command
{
    using Framework.Business;
    using Framework.Business.Extension;
    using Framework.Business.Manager.Command;
    using Framework.Business.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class CommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel> : BaseCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity>, ICommandManager<TErrorCode, TCreateModel, TUpdateModel>
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TErrorCode : struct, Enum
        where TEntity : BaseEntity, IEntityWithId
        where TCreateModel : class, IModel
        where TUpdateModel : class, TCreateModel, IModelWithId
    {
        private readonly IGenericQueryRepository<TReadOnlyDbContext, TEntity> _queryRepository;
        private readonly IGenericCommandRepository<TDbContext, TEntity> _commandRepository;

        protected CommandManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryRepository,
            IGenericCommandRepository<TDbContext, TEntity> commandRepository, ModelValidator<TCreateModel> createModelValidator, ModelValidator<TUpdateModel> updateModelValidator, ILogger<CommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>> logger, IMapper mapper, TErrorCode idDoesNotExist, TErrorCode idNotUnique)
            : base(queryRepository, commandRepository, logger, idDoesNotExist)
        {
            EnsureArg.IsNotNull(createModelValidator, nameof(createModelValidator));
            EnsureArg.IsNotNull(updateModelValidator, nameof(updateModelValidator));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            CreateModelValidator = createModelValidator;
            UpdateModelValidator = updateModelValidator;
            Mapper = mapper;
            IdNotUnique = idNotUnique;
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        protected TErrorCode IdNotUnique { get; }

        protected IMapper Mapper { get; }

        protected ModelValidator<TCreateModel> CreateModelValidator { get; }

        protected ModelValidator<TUpdateModel> UpdateModelValidator { get; }

        public async Task<ManagerResponse<TErrorCode>> CreateAsync(TCreateModel model, params TCreateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await CreateAsync(models.Prepend(model)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        public virtual async Task<ManagerResponse<TErrorCode>> CreateAsync(IEnumerable<TCreateModel> models)
        {
            try
            {
                ValidateModel(models);
                var indexedModels = models.ToIndexedItems().ToList();
                var errorRecords = CreateModelValidator.ExecuteCreateValidation<TErrorCode, TCreateModel>(indexedModels);
                var customErrorRecords = await CreateValidationAsync(indexedModels).ConfigureAwait(false);

                return await CreateOrUpdateAsync(models, errorRecords, customErrorRecords, async entities =>
                {
                    await CreateAfterMapAsync(indexedModels, entities).ConfigureAwait(false);
                    await _commandRepository.Insert(entities).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        public async Task<ManagerResponse<TErrorCode>> UpdateAsync(TUpdateModel model, params TUpdateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await UpdateAsync(models.Prepend(model).ToArray()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(UpdateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        public virtual async Task<ManagerResponse<TErrorCode>> UpdateAsync(IEnumerable<TUpdateModel> models)
        {
            try
            {
                ValidateModel(models);
                var indexedModels = models.ToIndexedItems().ToList();
                var errorRecords = UpdateModelValidator.ExecuteUpdateValidation<TErrorCode, TUpdateModel>(indexedModels);
                var customErrorRecords = await UpdateValidationAsync(indexedModels).ConfigureAwait(false);

                return await CreateOrUpdateAsync(models, errorRecords, customErrorRecords, async entities =>
                {
                    await UpdateAfterMapAsync(indexedModels, entities).ConfigureAwait(false);
                    await _commandRepository.Update(entities).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(UpdateAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        public async Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(long id, params long[] ids)
        {
            try
            {
                EnsureArg.IsGt(id, 0, nameof(id));

                return await DeleteByIdAsync(ids.Prepend(id)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIdAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        public virtual async Task<ManagerResponse<TErrorCode>> DeleteByIdAsync(IEnumerable<long> ids)
        {
            try
            {
                EnsureArg.IsNotNull(ids, nameof(ids));
                EnsureArgExtensions.HasItems(ids, nameof(ids));

                return await DeleteByExpressionAsync(ids, x => ids.Contains(x.Id)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByIdAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        protected virtual async Task<ErrorRecords<TErrorCode>> CreateValidationAsync(IList<IIndexedItem<TCreateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(CreateValidationAsync)}");

            return await Task.FromResult(new ErrorRecords<TErrorCode>()).ConfigureAwait(false);
        }

        protected virtual async Task<ErrorRecords<TErrorCode>> UpdateValidationAsync(IList<IIndexedItem<TUpdateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(UpdateValidationAsync)}");

            var existsValidationError = await ValidationHelpers.ExistsValidationAsync(
                async (keys) =>
                {
                    var modelIds = keys.Select(x => x).ToList();

                    var result = await _queryRepository.FetchByAsync(x => modelIds.Contains(x.Id), x => new IdKey<long>(x.Id, x.Id)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.Id,
                IdDoesNotExist).ConfigureAwait(false);

            var duplicateErrorIds = ValidationHelpers.DuplicateValidation(indexedModels, item => item.Item.Id, IdNotUnique);
            return new ErrorRecords<TErrorCode>(existsValidationError.Concat(duplicateErrorIds));
        }

        protected virtual Task CreateAfterMapAsync(IList<IIndexedItem<TCreateModel>> indexedItems, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        protected virtual Task UpdateAfterMapAsync(IList<IIndexedItem<TUpdateModel>> indexedItems, IList<TEntity> entities)
        {
            return Task.CompletedTask;
        }

        private async Task<ManagerResponse<TErrorCode>> CreateOrUpdateAsync<TModel>(IEnumerable<TModel> models, ErrorRecords<TErrorCode> errorRecords, IEnumerable<ErrorRecord<TErrorCode>> customErrorRecords, Func<List<TEntity>, Task> entityDatabaseOperation)
        {
            var mergedErrorRecords = errorRecords.Merge(customErrorRecords);

            if (mergedErrorRecords.Any())
            {
                return new ManagerResponse<TErrorCode>(mergedErrorRecords);
            }

            var entities = new List<TEntity>();
            Mapper.Map(models, entities);
            await entityDatabaseOperation(entities).ConfigureAwait(false);
            return new ManagerResponse<TErrorCode>(entities.Select(x => x.Id));
        }

        private void ValidateModel<TModel>(IEnumerable<TModel> models)
        {
            EnsureArg.IsNotNull(models, nameof(models));
            EnsureArgExtensions.HasItems(models, nameof(models));
        }
    }
}
