namespace Framework.Business.Extension
{
    using Framework.Business.Models;
    using System;

    public static class IndexedItemsExtensions
    {
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
