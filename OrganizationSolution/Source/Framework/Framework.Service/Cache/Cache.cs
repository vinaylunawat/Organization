namespace Framework.Service.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading;
    static internal class Cache
    {
        private static readonly ObjectCache cache = MemoryCache.Default;
        private static readonly ReaderWriterLockSlim MemoryCacheLock = new ReaderWriterLockSlim();

        static internal bool Contains(string key)
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    return cache.Contains(key);
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }

        static internal T Get<T>(string key)
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    return !cache.Contains(key) ? default : (T)cache[key];
                }
            }
            catch
            {
                return default;
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }

        static internal void Put(string key, object value)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy { Priority = CacheItemPriority.NotRemovable };
            Put(key, value, cacheItemPolicy);
        }

        static internal void Put(string key, object value, TimeSpan cacheExpiration)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy { SlidingExpiration = cacheExpiration };
            Put(key, value, cacheItemPolicy);
        }

        private static void Put(string key, object value, CacheItemPolicy cacheItemPolicy)
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    cache.Set(key, value, cacheItemPolicy);
                }
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }

        static internal long GetKeyCountFromCache()
        {
            try
            {
                MemoryCacheLock.EnterReadLock();
                {
                    return cache.GetCount();
                }
            }
            finally
            {
                MemoryCacheLock.ExitReadLock();
            }
        }

        static internal void RemoveAll()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys) MemoryCache.Default.Remove(cacheKey);
        }

        static internal void Remove(string key)
        {
            try
            {
                MemoryCacheLock.EnterWriteLock();
                {
                    if (cache.Contains(key)) cache.Remove(key);
                }
            }
            finally
            {
                MemoryCacheLock.ExitWriteLock();
            }
        }
    }
}
