namespace Framework.Service.Cache
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;

    public abstract class AbstractCache<TCachedEntity> : ICache<TCachedEntity> where TCachedEntity : class
    {
        private TimeSpan cacheExpiration;
        private string cacheKey;
        private object lockObject;
        private readonly ILogger<AbstractCache<TCachedEntity>> _logger;
        protected AbstractCache(ILogger<AbstractCache<TCachedEntity>> logger, string cacheItemKey, TimeSpan individualCacheExpiration)
        {
            Initialize(cacheItemKey, individualCacheExpiration);
            _logger = logger;
        }

        public List<TCachedEntity> GetFromCache()
        {
            LoadCache();
            return Cache.Get<List<TCachedEntity>>(cacheKey);
        }

        public void LoadCache()
        {
            if (Exists) return;
            RefreshCache();
        }

        public void RefreshCache()
        {
            SaveToCache(GetCacheData());
        }

        public void RemoveKeyFromCache()
        {
            if (!Exists) return;
            Cache.Remove(cacheKey);
        }

        public void RemoveAllKeysFromCache()
        {
            Cache.RemoveAll();
        }

        public long GetKeyCountFromCache()
        {
            return Cache.GetKeyCountFromCache();
        }

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

        public void Dispose()
        {
            if (!Exists) return;
            Cache.Remove(cacheKey);
        }        

        public bool Exists
        {
            get
            {
                lock (lockObject) return Cache.Contains(cacheKey);
            }
        }       

        private void Initialize(string cacheItemKey, TimeSpan individualCacheExpiration)
        {
            cacheKey = cacheItemKey;
            cacheExpiration = individualCacheExpiration;
            lockObject = new object();
        }

        public abstract List<TCachedEntity> GetCacheData();
    }
}
