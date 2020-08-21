namespace Framework.Business.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Framework.Business.Models;
    using EnsureThat;
    using FluentValidation;
    using FluentValidation.Results;

    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorEnum<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Enum errorCode)
        {
            return rule.WithErrorCode(errorCode.ToString());
        }

        public static ValidationFailure WithErrorEnum(this ValidationFailure validationFailure, Enum errorCode)
        {
            validationFailure.ErrorCode = errorCode.ToString();

            return validationFailure;
        }

        public static ErrorMessages<TErrorCode> ToErrorMessages<TErrorCode>(this ValidationResult validationResult)
            where TErrorCode : struct, Enum
        {
            var errorMessages = new ErrorMessages<TErrorCode>();

            foreach (var error in validationResult.Errors)
            {
                errorMessages.Add(new ErrorMessage<TErrorCode>(error));
            }

            return errorMessages;
        }

        public static ErrorRecords<TErrorCode> ExecuteUpdateValidation<TErrorCode, TModel>(this ModelValidator<TModel> validator, IIndexedItem<TModel> indexedModel, params IIndexedItem<TModel>[] indexedModels)
            where TErrorCode : struct, Enum
            where TModel : class, IModelWithId
        {
            EnsureArg.IsNotNull(indexedModel, nameof(indexedModel));
            return ExecuteUpdateValidation<TErrorCode, TModel>(validator, indexedModels.Prepend(indexedModel).ToArray());
        }

        public static ErrorRecords<TErrorCode> ExecuteUpdateValidation<TErrorCode, TModel>(this ModelValidator<TModel> validator, IList<IIndexedItem<TModel>> indexedModels)
            where TErrorCode : struct, Enum
            where TModel : class, IModelWithId
        {
            EnsureArg.IsNotNull(validator, nameof(validator));
            EnsureArg.IsNotNull(indexedModels, nameof(indexedModels));

            var errorRecords = new ErrorRecords<TErrorCode>();

            foreach (var indexedModel in indexedModels)
            {
                var result = validator.Validate(indexedModel.Item);

                if (!result.IsValid)
                {
                    errorRecords.Add(new ErrorRecord<TErrorCode>(indexedModel.Item.Id, indexedModel.OrdinalPosition, result));
                }
            }

            return errorRecords;
        }

        public static ErrorRecords<TErrorCode> ExecuteCreateValidation<TErrorCode, TModel>(this ModelValidator<TModel> validator, IIndexedItem<TModel> indexedModel, params IIndexedItem<TModel>[] indexedModels)
            where TErrorCode : struct, Enum
            where TModel : class, IModel
        {
            EnsureArg.IsNotNull(indexedModel, nameof(indexedModel));
            return ExecuteCreateValidation<TErrorCode, TModel>(validator, indexedModels.Prepend(indexedModel).ToArray());
        }

        public static ErrorRecords<TErrorCode> ExecuteCreateValidation<TErrorCode, TModel>(this ModelValidator<TModel> validator, IList<IIndexedItem<TModel>> indexedModels)
            where TErrorCode : struct, Enum
            where TModel : class, IModel
        {
            EnsureArg.IsNotNull(validator, nameof(validator));
            EnsureArg.IsNotNull(indexedModels, nameof(indexedModels));

            var errorRecords = new ErrorRecords<TErrorCode>();

            foreach (var indexedModel in indexedModels)
            {
                var result = validator.Validate(indexedModel.Item);

                if (!result.IsValid)
                {
                    errorRecords.Add(new ErrorRecord<TErrorCode>(indexedModel.OrdinalPosition, result));
                }
            }

            return errorRecords;
        }

        public static void ThrowIfError<TErrorCode>(this ManagerResponseBase<TErrorCode> managerResponse, string message = null)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(managerResponse, nameof(managerResponse));

            if (managerResponse.HasError)
            {
                var output = managerResponse.ErrorRecords.ToFormattedString();

                if (message != null)
                {
                    output = string.Concat(output, Environment.NewLine, message);
                }

                throw new InvalidOperationException(output);
            }
        }

        /// <summary>
        /// Merges the first set with the second set.
        /// DO NOT call merge more than once. Use Concat to append multiple sets prior to a merge.
        /// </summary>
        /// <typeparam name="TErrorCode">The type of the error code.</typeparam>
        /// <param name="first">The first set of errorRecords.</param>
        /// <param name="second">The second set of errorRecords.</param>
        /// <returns>ErrorRecords{TErrorCode}</returns>
        public static ErrorRecords<TErrorCode> Merge<TErrorCode>(this IEnumerable<ErrorRecord<TErrorCode>> first, IEnumerable<ErrorRecord<TErrorCode>> second)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(first, nameof(first));
            EnsureArg.IsNotNull(second, nameof(second));

            return new ErrorRecords<TErrorCode>(first.Concat(second).GroupBy(x => x.OrdinalPosition)
                .Select(x => new ErrorRecord<TErrorCode>(x.First().Id, x.Key, x.SelectMany(record => record.Errors)))
                .OrderBy(x => x.OrdinalPosition));
        }

        /// <summary>
        /// Merges the set.
        /// DO NOT call merge more than once. Use Concat to append multiple sets prior to a merge.
        /// </summary>
        /// <typeparam name="TErrorCode">The type of the error code.</typeparam>
        /// <param name="errorRecords">The errorRecords.</param>
        /// <returns>ErrorRecords{TErrorCode}</returns>
        public static ErrorRecords<TErrorCode> Merge<TErrorCode>(this IEnumerable<ErrorRecord<TErrorCode>> errorRecords)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(errorRecords, nameof(errorRecords));

            return Merge(Enumerable.Empty<ErrorRecord<TErrorCode>>(), errorRecords);
        }

        public static IEnumerable<GroupedItem<TItem, TKey>> ToGroupedItems<TItem, TKey>(this IEnumerable<TItem> items, Func<TItem, TKey> groupByMember)
        {
            return items.GroupBy(x => groupByMember.Invoke(x))
                .Select(y => new GroupedItem<TItem, TKey>(y.Key, y));
        }

        public static IEnumerable<IIndexedItem<TItem>> FindDuplicates<TItem, TKey>(this IEnumerable<IIndexedItem<TItem>> indexedItems, Func<IIndexedItem<TItem>, TKey> groupByMember)
        {
            return indexedItems.GroupBy(groupByMember).SelectMany(y => y.Skip(1));
        }

        public static IEnumerable<IIndexedItem<TItem>> ToIndexedItems<TItem, TKey>(this IEnumerable<TItem> items, IEnumerable<TKey> keys, Func<TItem, TKey> keySelector)
        {
            var theKeys = keys.ToList();
            return items.Select(x => new IndexedItem<TItem>(theKeys.IndexOf(keySelector.Invoke(x)), x))
                .Where(x => x.OrdinalPosition != -1)
                .OrderBy(x => x.OrdinalPosition);
        }

        public static IEnumerable<IIndexedItem<TItem>> ToIndexedItems<TItem>(this IEnumerable<TItem> items)
        {
            return items.Select((x, index) => new IndexedItem<TItem>(index, x));
        }

        public static IIndexedItem<TItem> ToIndexedItem<TItem>(this TItem item)
        {
            return new IndexedItem<TItem>(0, item);
        }

        private static string ToFormattedString<TErrorCode>(this IEnumerable<ErrorRecord<TErrorCode>> errorRecords)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(errorRecords, nameof(errorRecords));

            var stringBuilder = new StringBuilder();

            foreach (var errorRecord in errorRecords)
            {
                stringBuilder.AppendLine(errorRecord.ToFormattedString());
            }

            return stringBuilder.ToString();
        }
    }
}
