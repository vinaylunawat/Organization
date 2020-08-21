namespace Framework.Business.Extension
{
    using Framework.Business.Models;
    using System;

    /// <summary>
    /// Defines the <see cref="IndexedItemsExtensions" />.
    /// </summary>
    public static class IndexedItemsExtensions
    {
        /// <summary>
        /// The CreateErrorMessage.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <typeparam name="TErrorCode">.</typeparam>
        /// <param name="indexedItem">The indexedItem<see cref="IIndexedItem{T}"/>.</param>
        /// <param name="propertyName">The propertyName<see cref="string"/>.</param>
        /// <param name="errorCode">The errorCode<see cref="TErrorCode"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="attemptedValue">The attemptedValue<see cref="object"/>.</param>
        /// <returns>The <see cref="ErrorRecord{TErrorCode}"/>.</returns>
        public static ErrorRecord<TErrorCode> CreateErrorMessage<T, TErrorCode>(this IIndexedItem<T> indexedItem, string propertyName, TErrorCode errorCode, string message, object attemptedValue)
            where TErrorCode : struct, Enum
        {
            var model = indexedItem.Item as IModelWithId;
            var errorMessage = new ErrorMessage<TErrorCode>(propertyName, errorCode, message, attemptedValue);
            if (model is null)
            {
                return new ErrorRecord<TErrorCode>(indexedItem.OrdinalPosition, errorMessage);
            }
            else
            {
                return new ErrorRecord<TErrorCode>(model.Id, indexedItem.OrdinalPosition, errorMessage);
            }
        }
    }
}
