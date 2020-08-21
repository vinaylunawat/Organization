namespace Framework.Service.Cache
{
    using System;
    using System.Collections.Generic;

    public interface ICache<TCachedEntity> : IDisposable where TCachedEntity : class
    {
        bool Exists { get; }
        List<TCachedEntity> GetFromCache();
        void SaveToCache(List<TCachedEntity> objectsToSave);
        void LoadCache();
        void RefreshCache();
        void RemoveKeyFromCache();
        void RemoveAllKeysFromCache();
        long GetKeyCountFromCache();
    }
}
