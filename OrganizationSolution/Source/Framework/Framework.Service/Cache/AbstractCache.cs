namespace Framework.Service.Cache
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AbstractCache{TCachedEntity}" />.
    /// </summary>
    /// <typeparam name="TCachedEntity">.</typeparam>
    public abstract class AbstractCache<TCachedEntity> : ICache<TCachedEntity> where TCachedEntity : class
    {
        /// <summary>
        /// Defines the cacheExpiration.
        /// </summary>
        private TimeSpan cacheExpiration;

        /// <summary>
        /// Defines the cacheKey.
        /// </summary>
        private string cacheKey;

        /// <summary>
        /// Defines the lockObject.
        /// </summary>
        private object lockObject;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<AbstractCache<TCachedEntity>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCache{TCachedEntity}"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{AbstractCache{TCachedEntity}}"/>.</param>
        /// <param name="cacheItemKey">The cacheItemKey<see cref="string"/>.</param>
        /// <param name="individualCacheExpiration">The individualCacheExpiration<see cref="TimeSpan"/>.</param>
        protected AbstractCache(ILogger<AbstractCache<TCachedEntity>> logger, string cacheItemKey, TimeSpan individualCacheExpiration)
        {
            Initialize(cacheItemKey, individualCacheExpiration);
            _logger = logger;
        }

        /// <summary>
        /// The GetFromCache.
        /// </summary>
        /// <returns>The <see cref="List{TCachedEntity}"/>.</returns>
        public List<TCachedEntity> GetFromCache()
        {
            LoadCache();
            return Cache.Get<List<TCachedEntity>>(cacheKey);
        }

        /// <summary>
        /// The LoadCache.
        /// </summary>
        public void LoadCache()
        {
            if (Exists) return;
            RefreshCache();
        }

        /// <summary>
        /// The RefreshCache.
        /// </summary>
        public void RefreshCache()
        {
            SaveToCache(GetCacheData());
        }

        /// <summary>
        /// The RemoveKeyFromCache.
        /// </summary>
        public void RemoveKeyFromCache()
        {
            if (!Exists) return;
            Cache.Remove(cacheKey);
        }

        /// <summary>
        /// The RemoveAllKeysFromCache.
        /// </summary>
        public void RemoveAllKeysFromCache()
        {
            Cache.RemoveAll();
        }

        /// <summary>
        /// The GetKeyCountFromCache.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetKeyCountFromCache()
        {
            return Cache.GetKeyCountFromCache();
        }

        /// <summary>
        /// The SaveToCache.
        /// </summary>
        /// <param name="objectsToSave">The objectsToSave<see cref="List{TCachedEntity}"/>.</param>
        public void SaveToCache(List<TCachedEntity> objectsToSave)
        {
            try
            {
                Cache.Put(cacheKey, objectsToSave, cacheExpiration);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Error in cache");
            }
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            if (!Exists) return;
            Cache.Remove(cacheKey);
        }

        /// <summary>
        /// Gets a value indicating whether Exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                lock (lockObject) return Cache.Contains(cacheKey);
            }
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        /// <param name="cacheItemKey">The cacheItemKey<see cref="string"/>.</param>
        /// <param name="individualCacheExpiration">The individualCacheExpiration<see cref="TimeSpan"/>.</param>
        private void Initialize(string cacheItemKey, TimeSpan individualCacheExpiration)
        {
            cacheKey = cacheItemKey;
            cacheExpiration = individualCacheExpiration;
            lockObject = new object();
        }

        /// <summary>
        /// The GetCacheData.
        /// </summary>
        /// <returns>The <see cref="List{TCachedEntity}"/>.</returns>
        public abstract List<TCachedEntity> GetCacheData();
    }
}
