namespace Framework.Business
{
    using EnsureThat;
    using FluentValidation;
    using Framework.Business.Extension;
    using Framework.Business.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ValidationHelpers" />.
    /// </summary>
    public static class ValidationHelpers
    {
        /// <summary>
        /// The UniqueValidationAsync.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="databaseSelector">The databaseSelector<see cref="Func{IList{TCompareType}, Task{IList{IdKey{TCompareType}}}}"/>.</param>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="modelPredicateExpression">The modelPredicateExpression<see cref="Expression{Func{IIndexedItem{TModel}, TCompareType}}"/>.</param>
        /// <param name="keyNotUnique">The keyNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="propertyNameExpression">The propertyNameExpression<see cref="Expression{Func{TModel, object}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        public static async Task<ErrorRecords<TErrorCode>> UniqueValidationAsync<TModel, TCompareType, TErrorCode>(Func<IList<TCompareType>, Task<IList<IdKey<TCompareType>>>> databaseSelector, IList<IIndexedItem<TModel>> indexedModels, Expression<Func<IIndexedItem<TModel>, TCompareType>> modelPredicateExpression, TErrorCode keyNotUnique, Expression<Func<TModel, object>> propertyNameExpression = null)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(databaseSelector, nameof(databaseSelector));
            EnsureArg.IsNotNull(indexedModels, nameof(indexedModels));
            EnsureArg.IsNotNull(modelPredicateExpression, nameof(modelPredicateExpression));

            var modelPredicate = modelPredicateExpression.Compile();
            var propertyName = GetPropertyName(propertyNameExpression, modelPredicateExpression);
            var message = $"{GetPropertyMessageName(propertyName)} must be unique.";

            var errorRecords = DuplicateValidation(indexedModels, modelPredicate, keyNotUnique, propertyName, message);

            var modelKeys = indexedModels.Select(modelPredicate).ToList();

            var existingIdKeys = await databaseSelector(modelKeys).ConfigureAwait(false);
            foreach (var existingIdKey in existingIdKeys)
            {
                foreach (var index in modelKeys.AllIndexesOf(existingIdKey.Key))
                {
                    var duplicate = indexedModels[index];

                    if (!errorRecords.Any(x => x.OrdinalPosition == duplicate.OrdinalPosition))
                    {
                        var errorMessage = new ErrorMessage<TErrorCode>(propertyName, keyNotUnique, message, existingIdKey.Key);
                        errorRecords.Add(new ErrorRecord<TErrorCode>(duplicate.OrdinalPosition, errorMessage));
                    }
                }
            }

            return errorRecords;
        }

        /// <summary>
        /// The UniqueWithIdValidationAsync.
        /// </summary>
        /// <typeparam name="TUpdateModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="databaseSelector">The databaseSelector<see cref="Func{IList{TCompareType}, Task{IList{IdKey{TCompareType}}}}"/>.</param>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TUpdateModel}}"/>.</param>
        /// <param name="modelPredicateExpression">The modelPredicateExpression<see cref="Expression{Func{IIndexedItem{TUpdateModel}, TCompareType}}"/>.</param>
        /// <param name="keyNotUnique">The keyNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="propertyNameExpression">The propertyNameExpression<see cref="Expression{Func{TUpdateModel, object}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        public static async Task<ErrorRecords<TErrorCode>> UniqueWithIdValidationAsync<TUpdateModel, TCompareType, TErrorCode>(Func<IList<TCompareType>, Task<IList<IdKey<TCompareType>>>> databaseSelector, IList<IIndexedItem<TUpdateModel>> indexedModels, Expression<Func<IIndexedItem<TUpdateModel>, TCompareType>> modelPredicateExpression, TErrorCode keyNotUnique, Expression<Func<TUpdateModel, object>> propertyNameExpression = null)
            where TErrorCode : struct, Enum
            where TUpdateModel : class, IModelWithId
        {
            EnsureArg.IsNotNull(databaseSelector, nameof(databaseSelector));
            EnsureArg.IsNotNull(indexedModels, nameof(indexedModels));
            EnsureArg.IsNotNull(modelPredicateExpression, nameof(modelPredicateExpression));

            var modelPredicate = modelPredicateExpression.Compile();
            var propertyName = GetPropertyName(propertyNameExpression, modelPredicateExpression);
            var message = $"{GetPropertyMessageName(propertyName)} must be unique.";

            var errorRecords = DuplicateValidation(indexedModels, modelPredicate, keyNotUnique, propertyName, message);

            var modelKeys = indexedModels.Select(modelPredicate).ToList();
            var existingIdKeys = await databaseSelector(modelKeys).ConfigureAwait(false);

            foreach (var existingIdKey in existingIdKeys)
            {
                foreach (var index in modelKeys.AllIndexesOf(existingIdKey.Key))
                {
                    var duplicate = indexedModels[index];
                    if (duplicate.Item.Id != existingIdKey.Id && !errorRecords.Any(x => x.OrdinalPosition == duplicate.OrdinalPosition))
                    {
                        var errorMessage = new ErrorMessage<TErrorCode>(propertyName, keyNotUnique, message, existingIdKey.Key);
                        errorRecords.Add(new ErrorRecord<TErrorCode>(duplicate.Item.Id, duplicate.OrdinalPosition, errorMessage));
                    }
                }
            }

            return errorRecords;
        }

        /// <summary>
        /// The ExistsValidationAsync.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="databaseSelector">The databaseSelector<see cref="Func{IList{TCompareType}, Task{IList{IdKey{TCompareType}}}}"/>.</param>
        /// <param name="indexedModels">The indexedModels<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="modelPredicateExpression">The modelPredicateExpression<see cref="Expression{Func{IIndexedItem{TModel}, TCompareType}}"/>.</param>
        /// <param name="keyDoesNotExist">The keyDoesNotExist<see cref="TErrorCode"/>.</param>
        /// <param name="propertyNameExpression">The propertyNameExpression<see cref="Expression{Func{TModel, object}}"/>.</param>
        /// <returns>The <see cref="Task{ErrorRecords{TErrorCode}}"/>.</returns>
        public static async Task<ErrorRecords<TErrorCode>> ExistsValidationAsync<TModel, TCompareType, TErrorCode>(Func<IList<TCompareType>, Task<IList<IdKey<TCompareType>>>> databaseSelector, IList<IIndexedItem<TModel>> indexedModels, Expression<Func<IIndexedItem<TModel>, TCompareType>> modelPredicateExpression, TErrorCode keyDoesNotExist, Expression<Func<TModel, object>> propertyNameExpression = null)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(databaseSelector, nameof(databaseSelector));
            EnsureArg.IsNotNull(indexedModels, nameof(indexedModels));
            EnsureArg.IsNotNull(modelPredicateExpression, nameof(modelPredicateExpression));

            var modelPredicate = modelPredicateExpression.Compile();
            var propertyName = GetPropertyName(propertyNameExpression, modelPredicateExpression);
            var propertyMessageName = GetPropertyMessageName(propertyName);
            var message = $"{propertyMessageName} does not exist.";

            var errorRecords = new ErrorRecords<TErrorCode>();
            var modelKeys = indexedModels.Select(modelPredicate).ToList();
            var existingKeys = await databaseSelector(modelKeys).ConfigureAwait(false);

            var missingKeys = modelKeys.Except(existingKeys.Select(x => x.Key)).ToList();
            if (missingKeys.Any())
            {
                foreach (var missingKey in missingKeys)
                {
                    foreach (var index in modelKeys.AllIndexesOf(missingKey))
                    {
                        errorRecords.Add(indexedModels[index].CreateErrorMessage(propertyName, keyDoesNotExist, message, missingKey));
                    }
                }
            }

            return errorRecords;
        }

        /// <summary>
        /// The DuplicateValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="modelPredicate">The modelPredicate<see cref="Func{IIndexedItem{TModel}, TCompareType}"/>.</param>
        /// <param name="keyNotUnique">The keyNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="ErrorRecords{TErrorCode}"/>.</returns>
        public static ErrorRecords<TErrorCode> DuplicateValidation<TModel, TCompareType, TErrorCode>(IList<IIndexedItem<TModel>> indexedItems, Func<IIndexedItem<TModel>, TCompareType> modelPredicate, TErrorCode keyNotUnique, string propertyName, string message)
            where TErrorCode : struct, Enum
        {
            var errorRecords = new ErrorRecords<TErrorCode>();
            var duplicateIndexedItem = indexedItems.FindDuplicates(modelPredicate);

            if (duplicateIndexedItem.Any())
            {
                foreach (var duplicate in duplicateIndexedItem)
                {
                    errorRecords.Add(duplicate.CreateErrorMessage(propertyName, keyNotUnique, message, modelPredicate.Invoke(duplicate)));
                }
            }

            return errorRecords;
        }

        /// <summary>
        /// The DuplicateValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="modelPredicateExpression">The modelPredicateExpression<see cref="Expression{Func{IIndexedItem{TModel}, TCompareType}}"/>.</param>
        /// <param name="keyNotUnique">The keyNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="propertyNameExpression">The propertyNameExpression<see cref="Expression{Func{TModel, object}}"/>.</param>
        /// <returns>The <see cref="ErrorRecords{TErrorCode}"/>.</returns>
        public static ErrorRecords<TErrorCode> DuplicateValidation<TModel, TCompareType, TErrorCode>(IList<IIndexedItem<TModel>> indexedItems, Expression<Func<IIndexedItem<TModel>, TCompareType>> modelPredicateExpression, TErrorCode keyNotUnique, Expression<Func<TModel, object>> propertyNameExpression = null)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(indexedItems, nameof(indexedItems));
            EnsureArg.IsNotNull(modelPredicateExpression, nameof(modelPredicateExpression));

            var modelPredicate = modelPredicateExpression.Compile();

            return DuplicateValidation(indexedItems, modelPredicate, keyNotUnique, propertyNameExpression);
        }

        /// <summary>
        /// The IsPreciseToDay.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalDayNotAllowedErrorCode">The fractionalDayNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToDay<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalDayNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Hour == 0 && x.Minute == 0 && x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalDayNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToDay.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalDayNotAllowedErrorCode">The fractionalDayNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToDay<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalDayNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Hour == 0 && x.Value.Minute == 0 && x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalDayNotAllowedErrorCode).WithMessage("Fractional hours are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToHours.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalHoursNotAllowedErrorCode">The fractionalHoursNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToHours<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalHoursNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Minute == 0 && x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalHoursNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToHours.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalHoursNotAllowedErrorCode">The fractionalHoursNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToHours<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalHoursNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Minute == 0 && x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalHoursNotAllowedErrorCode).WithMessage("Fractional hours are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToMinutes.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalMinutesNotAllowedErrorCode">The fractionalMinutesNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToMinutes<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalMinutesNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Second == 0 && x.Millisecond == 0).WithErrorEnum(fractionalMinutesNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToMinutes.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalMinutesNotAllowedErrorCode">The fractionalMinutesNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToMinutes<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalMinutesNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || (x.Value.Second == 0 && x.Value.Millisecond == 0)).WithErrorEnum(fractionalMinutesNotAllowedErrorCode).WithMessage("Fractional minutes are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToSeconds.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset}"/>.</param>
        /// <param name="fractionalSecondsNotAllowedErrorCode">The fractionalSecondsNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset> IsPreciseToSeconds<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, TErrorCode fractionalSecondsNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => x.Millisecond == 0).WithErrorEnum(fractionalSecondsNotAllowedErrorCode).WithMessage("Fractional seconds are not allowed.");
        }

        /// <summary>
        /// The IsPreciseToSeconds.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="ruleBuilder">The ruleBuilder<see cref="IRuleBuilder{T, DateTimeOffset?}"/>.</param>
        /// <param name="fractionalSecondsNotAllowedErrorCode">The fractionalSecondsNotAllowedErrorCode<see cref="TErrorCode"/>.</param>
        /// <returns>The <see cref="IRuleBuilderOptions{T, DateTimeOffset?}"/>.</returns>
        public static IRuleBuilderOptions<T, DateTimeOffset?> IsPreciseToSeconds<T, TErrorCode>(this IRuleBuilder<T, DateTimeOffset?> ruleBuilder, TErrorCode fractionalSecondsNotAllowedErrorCode)
            where TErrorCode : Enum
        {
            return ruleBuilder.Must(x => !x.HasValue || x.Value.Millisecond == 0).WithErrorEnum(fractionalSecondsNotAllowedErrorCode).WithMessage("Fractional seconds are not allowed.");
        }

        /// <summary>
        /// The DuplicateValidation.
        /// </summary>
        /// <typeparam name="TModel">.</typeparam>
        /// <typeparam name="TCompareType">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="indexedItems">The indexedItems<see cref="IList{IIndexedItem{TModel}}"/>.</param>
        /// <param name="modelPredicate">The modelPredicate<see cref="Func{IIndexedItem{TModel}, TCompareType}"/>.</param>
        /// <param name="keyNotUnique">The keyNotUnique<see cref="TErrorCode"/>.</param>
        /// <param name="propertyNameExpression">The propertyNameExpression<see cref="Expression{Func{TModel, object}}"/>.</param>
        /// <returns>The <see cref="ErrorRecords{TErrorCode}"/>.</returns>
        private static ErrorRecords<TErrorCode> DuplicateValidation<TModel, TCompareType, TErrorCode>(IList<IIndexedItem<TModel>> indexedItems, Func<IIndexedItem<TModel>, TCompareType> modelPredicate, TErrorCode keyNotUnique, Expression<Func<TModel, object>> propertyNameExpression = null)
            where TErrorCode : struct, Enum
        {
            EnsureArg.IsNotNull(indexedItems, nameof(indexedItems));
            EnsureArg.IsNotNull(modelPredicate, nameof(modelPredicate));

            var propertyName = GetPropertyName(propertyNameExpression);
            var propertyMessageName = GetPropertyMessageName(propertyName);
            var message = $"{propertyMessageName} must be unique.";

            return DuplicateValidation(indexedItems, modelPredicate, keyNotUnique, propertyName, message);
        }

        /// <summary>
        /// The GetPropertyMessageName.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyMessageName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return "Key";
            }
            else
            {
                return propertyName;
            }
        }

        /// <summary>
        /// The GetPropertyName.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <param name="nameExpression">The nameExpression<see cref="Expression{Func{T1, T2}}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyName<T1, T2>(Expression<Func<T1, T2>> nameExpression)
        {
            if (nameExpression?.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression memberExpression)
                {
                    return memberExpression.Member.Name;
                }
            }
            else if (nameExpression?.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// The GetPropertyName.
        /// </summary>
        /// <typeparam name="T1">.</typeparam>
        /// <typeparam name="T2">.</typeparam>
        /// <typeparam name="T3">.</typeparam>
        /// <typeparam name="T4">.</typeparam>
        /// <param name="nameExpression1">The nameExpression1<see cref="Expression{Func{T1, T2}}"/>.</param>
        /// <param name="nameExpression2">The nameExpression2<see cref="Expression{Func{T3, T4}}"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetPropertyName<T1, T2, T3, T4>(Expression<Func<T1, T2>> nameExpression1, Expression<Func<T3, T4>> nameExpression2)
        {
            string propertyName;
            if (nameExpression1 != null)
            {
                propertyName = GetPropertyName(nameExpression1);
            }
            else
            {
                propertyName = GetPropertyName(nameExpression2);
            }

            return propertyName;
        }

        /// <summary>
        /// The AllIndexesOf.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="items">The items<see cref="IEnumerable{T}"/>.</param>
        /// <param name="item">The item<see cref="T"/>.</param>
        /// <returns>The <see cref="IEnumerable{int}"/>.</returns>
        private static IEnumerable<int> AllIndexesOf<T>(this IEnumerable<T> items, T item)
        {
            return items.Select((x, i) => x.Equals(item) ? i : -1).Where(x => x != -1);
        }
    }
}
