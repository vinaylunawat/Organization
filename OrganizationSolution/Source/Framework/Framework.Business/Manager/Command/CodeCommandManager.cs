namespace Framework.Business.Manager.Command
{
    using AutoMapper;
    using EnsureThat;
    using FluentValidation;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Extension;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CodeCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}" />.
    /// </summary>
    /// <typeparam name="TDbContext">.</typeparam>
    /// <typeparam name="TReadOnlyDbContext">.</typeparam>
    /// <typeparam name="TErrorCode">.</typeparam>
    /// <typeparam name="TEntity">.</typeparam>
    /// <typeparam name="TCreateModel">.</typeparam>
    /// <typeparam name="TUpdateModel">.</typeparam>
    public abstract class CodeCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>
        : CommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>
        , ICodeCommandManager<TErrorCode, TCreateModel, TUpdateModel>
        where TDbContext : BaseDbContext<TDbContext>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TErrorCode : struct, Enum
        where TEntity : EntityWithId, IEntityWithCode
        where TCreateModel : class, IModelWithCode
        where TUpdateModel : class, TCreateModel, IModelWithId, IModelWithCode
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
        /// Initializes a new instance of the <see cref="CodeCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}"/> class.
        /// </summary>
        /// <param name="queryRepository">The queryRepository<see cref="IGenericQueryRepository{TReadOnlyDbContext, TEntity}"/>.</param>
        /// <param name="commandRepository">The commandRepository<see cref="IGenericCommandRepository{TDbContext, TEntity}"/>.</param>
        /// <param name="createModelValidator">The createModelValidator<see cref="ModelValidator{TCreateModel}"/>.</param>
        /// <param name="updateModelValidator">The updateModelValidator<see cref="ModelValidator{TUpdateModel}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{CodeCommandManager{TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel}}"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="idDoesNotExist">The idDoesNotExist<see cref="TErrorCode"/>.</param>
        /// <param name="idNotUnique">The idNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="codeNotUnique">The codeNotUnique<see cref="TErrorCode"/>.</param>
        protected CodeCommandManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryRepository,
            IGenericCommandRepository<TDbContext, TEntity> commandRepository, ModelValidator<TCreateModel> createModelValidator, ModelValidator<TUpdateModel> updateModelValidator, ILogger<CodeCommandManager<TDbContext, TReadOnlyDbContext, TErrorCode, TEntity, TCreateModel, TUpdateModel>> logger, IMapper mapper, TErrorCode idDoesNotExist, TErrorCode idNotUnique, TErrorCode codeNotUnique)
            : base(queryRepository, commandRepository, createModelValidator, updateModelValidator, logger, mapper, idDoesNotExist, idNotUnique)
        {
            CodeNotUnique = codeNotUnique;
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        /// Gets the CodeNotUnique.
        /// </summary>
        protected TErrorCode CodeNotUnique { get; }

        /// <summary>
        /// The DeleteByCodeAsync.
        /// </summary>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <param name="codes">The codes<see cref="string[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> DeleteByCodeAsync(string code, params string[] codes)
        {
            try
            {
                EnsureArg.IsNotEmptyOrWhiteSpace(code, nameof(code));

                return await DeleteByCodeAsync(codes.Prepend(code)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The DeleteByCodeAsync.
        /// </summary>
        /// <param name="codes">The codes<see cref="IEnumerable{string}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> DeleteByCodeAsync(IEnumerable<string> codes)
        {
            try
            {
                EnsureArg.IsNotNull(codes, nameof(codes));
                EnsureArgExtensions.HasItems(codes, nameof(codes));

                return await DeleteByExpressionAsync(codes, x => codes.Contains(x.Code)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(DeleteByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateIfNotExistByCodeAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TCreateModel"/>.</param>
        /// <param name="models">The models<see cref="TCreateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> CreateIfNotExistByCodeAsync(TCreateModel model, params TCreateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await CreateIfNotExistByCodeAsync(models.Prepend(model)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateIfNotExistByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateIfNotExistByCodeAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TCreateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> CreateIfNotExistByCodeAsync(IEnumerable<TCreateModel> models)
        {
            try
            {
                EnsureArg.IsNotNull(models, nameof(models));
                EnsureArgExtensions.HasItems(models, nameof(models));

                var indexedModels = models.ToIndexedItems().ToList();
                var modelCodes = models.Select(y => y.Code);

                // Validate set for duplicates
                var duplicateCodes = ValidationHelpers.DuplicateValidation(indexedModels, item => item.Item.Code, CodeNotUnique);
                if (duplicateCodes.Any())
                {
                    return new ManagerResponse<TErrorCode>(duplicateCodes);
                }

                var existingKeys = await _queryRepository.FetchByAsync(x => modelCodes.Contains(x.Code), x => x.Code).ConfigureAwait(false);

                var missingIndexedModels = indexedModels.Where(x => !existingKeys.Select(y => y).Contains(x.Item.Code)).ToList();

                if (missingIndexedModels.Any())
                {
                    var errorRecords = CreateModelValidator.ExecuteCreateValidation<TErrorCode, TCreateModel>(missingIndexedModels);
                    var customErrorRecords = await CreateValidationAsync(missingIndexedModels).ConfigureAwait(false);

                    var mergedErrorRecords = errorRecords.Merge(customErrorRecords);

                    if (mergedErrorRecords.Any())
                    {
                        return new ManagerResponse<TErrorCode>(mergedErrorRecords);
                    }
                    else
                    {
                        var entities = new List<TEntity>();
                        Mapper.Map(missingIndexedModels.Select(x => x.Item), entities);
                        await CreateAfterMapAsync(missingIndexedModels, entities).ConfigureAwait(false);
                        await _commandRepository.Insert(entities).ConfigureAwait(false);
                    }
                }
                var finalIds = _queryRepository.FetchByAndReturnQuerable(x => modelCodes.Contains(x.Code)).OrderEntitiesByModelsOrder(models, entity => entity.Code, entity => entity.Id, model => model.Code);
                return new ManagerResponse<TErrorCode>(finalIds);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateIfNotExistByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateOrUpdateByCodeAsync.
        /// </summary>
        /// <param name="model">The model<see cref="TUpdateModel"/>.</param>
        /// <param name="models">The models<see cref="TUpdateModel[]"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public async Task<ManagerResponse<TErrorCode>> CreateOrUpdateByCodeAsync(TUpdateModel model, params TUpdateModel[] models)
        {
            try
            {
                EnsureArg.IsNotNull(model, nameof(model));

                return await CreateOrUpdateByCodeAsync(models.Prepend(model)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateOrUpdateByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateOrUpdateByCodeAsync.
        /// </summary>
        /// <param name="models">The models<see cref="IEnumerable{TUpdateModel}"/>.</param>
        /// <returns>The <see cref="Task{ManagerResponse{TErrorCode}}"/>.</returns>
        public virtual async Task<ManagerResponse<TErrorCode>> CreateOrUpdateByCodeAsync(IEnumerable<TUpdateModel> models)
        {
            try
            {
                EnsureArg.IsNotNull(models, nameof(models));
                EnsureArgExtensions.HasItems(models, nameof(models));

                var indexedModels = models.ToIndexedItems().ToList();
                var modelCodes = indexedModels.Select(y => y.Item.Code);
                var anyError = false;

                var existingEntities = await _queryRepository.FetchByAsync(x => modelCodes.Contains(x.Code), x => new { x.Id, x.Code }).ConfigureAwait(false);
                var updateIndexedModels = indexedModels.Join(existingEntities, indexedModel => indexedModel.Item.Code, entity => entity.Code, (model, entity) =>
                {
                    model.Item.Id = entity.Id;
                    return model;
                }).ToList();

                var errorRecords = Enumerable.Empty<ErrorRecord<TErrorCode>>();
                if (updateIndexedModels.Any())
                {
                    var updateErrorRecords = UpdateModelValidator.ExecuteUpdateValidation<TErrorCode, TUpdateModel>(updateIndexedModels);
                    var customUpdateErrorRecords = await UpdateValidationAsync(updateIndexedModels).ConfigureAwait(false);

                    errorRecords = errorRecords.Concat(updateErrorRecords).Concat(customUpdateErrorRecords);

                    if (errorRecords.Any())
                    {
                        anyError = true;
                    }
                    else
                    {
                        var updatedEntities = new List<TEntity>();

                        Mapper.Map(updateIndexedModels.Select(x => x.Item), updatedEntities);
                        await UpdateAfterMapAsync(updateIndexedModels, updatedEntities).ConfigureAwait(false);
                        _commandRepository.UpdateOnly(updatedEntities);
                    }
                }

                var existingCodes = existingEntities.Select(y => y.Code);

                var createIndexedModels = indexedModels.Where(x => !existingCodes.Contains(x.Item.Code)).Cast<IIndexedItem<TCreateModel>>().ToList();
                var createdEntities = new List<TEntity>();
                if (createIndexedModels.Any())
                {
                    var createErrorRecords = CreateModelValidator.ExecuteCreateValidation<TErrorCode, TCreateModel>(createIndexedModels);
                    var customCreateErrorRecords = await CreateValidationAsync(createIndexedModels).ConfigureAwait(false);

                    errorRecords = errorRecords.Concat(createErrorRecords).Concat(customCreateErrorRecords);

                    if (errorRecords.Any())
                    {
                        anyError = true;
                    }
                    else
                    {
                        Mapper.Map(createIndexedModels.Select(x => x.Item), createdEntities);
                        await CreateAfterMapAsync(createIndexedModels, createdEntities).ConfigureAwait(false);
                        await _commandRepository.InsertOnly(createdEntities).ConfigureAwait(false);
                    }
                }

                if (anyError)
                {
                    var mergedErrors = errorRecords.Merge();

                    return new ManagerResponse<TErrorCode>(new ErrorRecords<TErrorCode>(mergedErrors));
                }
                else
                {
                    await _commandRepository.SaveOnly().ConfigureAwait(false);

                    // update the models id
                    // must be after the save so the ids are correct
                    createdEntities.ForEach(x => indexedModels.Single(y => y.Item.Code == x.Code).Item.Id = x.Id);

                    var finalIds = indexedModels.Select(x => x.Item.Id);
                    return new ManagerResponse<TErrorCode>(finalIds);
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, nameof(CreateOrUpdateByCodeAsync));
                return new ManagerResponse<TErrorCode>(ex);
            }
        }

        /// <summary>
        /// The CreateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TCreateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected override async Task<ErrorRecords<TErrorCode>> CreateValidationAsync(IList<IIndexedItem<TCreateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(CreateValidationAsync)}");

            var baseErrorRecords = await base.CreateValidationAsync(indexedModels).ConfigureAwait(false);
            var uniqueErrorRecords = await ValidationHelpers.UniqueValidationAsync(
                async (keys) =>
                {
                    var result = await _queryRepository.FetchByAsync(x => keys.Contains(x.Code), x => new IdKey<string>(x.Id, x.Code)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.Code,
                CodeNotUnique).ConfigureAwait(false);

            return new ErrorRecords<TErrorCode>(baseErrorRecords.Concat(uniqueErrorRecords));
        }

        /// <summary>
        /// The UpdateValidationAsync.
        /// </summary>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        protected override async Task<ErrorRecords<TErrorCode>> UpdateValidationAsync(IList<IIndexedItem<TUpdateModel>> indexedModels)
        {
            Logger.LogDebug($"Calling {nameof(UpdateValidationAsync)}");

            var baseErrorRecords = await base.UpdateValidationAsync(indexedModels).ConfigureAwait(false);
            var uniqueErrorRecords = await ValidationHelpers.UniqueWithIdValidationAsync(
                async (keys) =>
                {
                    var result = await _queryRepository.FetchByAsync(x => keys.Contains(x.Code), x => new IdKey<string>(x.Id, x.Code)).ConfigureAwait(false);
                    return result.ToList();
                },
                indexedModels,
                x => x.Item.Code,
                CodeNotUnique).ConfigureAwait(false);

            return new ErrorRecords<TErrorCode>(baseErrorRecords.Concat(uniqueErrorRecords));
        }
    }
}
