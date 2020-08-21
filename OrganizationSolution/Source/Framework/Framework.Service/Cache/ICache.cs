namespace Framework.Service.Cache
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ICache{TCachedEntity}" />.
    /// </summary>
    /// <typeparam name="TCachedEntity">.</typeparam>
    public interface ICache<TCachedEntity> : IDisposable where TCachedEntity : class
    {
        /// <summary>
        /// Gets a value indicating whether Exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// The GetFromCache.
        /// </summary>
        /// <returns>The <see cref="List{TCachedEntity}"/>.</returns>
        List<TCachedEntity> GetFromCache();

        /// <summary>
        /// The SaveToCache.
        /// </summary>
        /// <param name="objectsToSave">The objectsToSave<see cref="List{TCachedEntity}"/>.</param>
        void SaveToCache(List<TCachedEntity> objectsToSave);

        /// <summary>
        /// The LoadCache.
        /// </summary>
        void LoadCache();

        /// <summary>
        /// The RefreshCache.
        /// </summary>
        void RefreshCache();

        /// <summary>
        /// The RemoveKeyFromCache.
        /// </summary>
        void RemoveKeyFromCache();

        /// <summary>
        /// The RemoveAllKeysFromCache.
        /// </summary>
        void RemoveAllKeysFromCache();

        /// <summary>
        /// The GetKeyCountFromCache.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        long GetKeyCountFromCache();
    }
}
