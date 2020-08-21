namespace Framework.Business.Manager.Query
{
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using Framework.DataAccess;
    using Framework.DataAccess.Repository;
    using Framework.Entity;
    using Framework.Service.Utilities.Criteria;
    using AutoMapper;
    using EnsureThat;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class CodeQueryManager<TReadOnlyDbContext, TEntity, TReadModel> : QueryManager<TReadOnlyDbContext, TEntity, TReadModel>, ICodeQueryManager<TReadModel>
        where TReadOnlyDbContext : BaseReadOnlyDbContext<TReadOnlyDbContext>
        where TEntity : BaseEntity, IEntityWithId, IEntityWithCode
        where TReadModel : class, IModelWithCode
    {
        protected CodeQueryManager(IGenericQueryRepository<TReadOnlyDbContext, TEntity> queryGenericRepository, ILogger<CodeQueryManager<TReadOnlyDbContext, TEntity, TReadModel>> logger, IMapper mapper)
            : base(queryGenericRepository, logger, mapper)
        {
        }

        public virtual async Task<IEnumerable<TReadModel>> GetByCodeAsync(string code, params string[] codes)
        {
            EnsureArg.IsNotNull(code, nameof(code));
            return await GetByCodeAsync(codes.Prepend(code)).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TReadModel>> GetByCodeAsync(IEnumerable<string> codes)
        {
            EnsureArg.IsNotNull(codes, nameof(codes));
            EnsureArgExtensions.HasItems(codes, nameof(codes));
            FilterCriteria<TEntity> filterCriteria = new FilterCriteria<TEntity>
            {
                Predicate = x => codes.Contains(x.Code)
            };
            return await GetByExpressionAsync(filterCriteria).ConfigureAwait(false);
        }
    }
}
