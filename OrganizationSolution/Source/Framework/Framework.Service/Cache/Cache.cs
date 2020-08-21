namespace Framework.Service.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Cache" />.
    /// </summary>
    static internal class Cache
    {
        /// <summary>
        /// Defines the cache.
        /// </summary>
        private static readonly ObjectCache cache = MemoryCache.Default;

        /// <summary>
        /// Defines the MemoryCacheLock.
        /// </summary>
        private static readonly ReaderWriterLockSlim MemoryCacheLock = new ReaderWriterLockSlim();

        /// <summary>
        /// The Contains.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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

        /// <summary>
        /// The Get.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <returns>The <see cref="T"/>.</returns>
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

        /// <summary>
        /// The Put.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        static internal void Put(string key, object value)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy { Priority = CacheItemPriority.NotRemovable };
            Put(key, value, cacheItemPolicy);
        }

        /// <summary>
        /// The Put.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="cacheExpiration">The cacheExpiration<see cref="TimeSpan"/>.</param>
        static internal void Put(string key, object value, TimeSpan cacheExpiration)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy { SlidingExpiration = cacheExpiration };
            Put(key, value, cacheItemPolicy);
        }

        /// <summary>
        /// The Put.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="cacheItemPolicy">The cacheItemPolicy<see cref="CacheItemPolicy"/>.</param>
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

        /// <summary>
        /// The GetKeyCountFromCache.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
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

        /// <summary>
        /// The RemoveAll.
        /// </summary>
        static internal void RemoveAll()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys) MemoryCache.Default.Remove(cacheKey);
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
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
