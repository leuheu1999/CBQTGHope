using System;
using System.Runtime.Caching;

namespace Module.Framework.UltimateClient
{
    public class InMemoryCache : ICacheService
    {
        public T Get<T>(string cacheKey) where T : class
        {
            return MemoryCache.Default.Get(cacheKey) as T;
        }

        public void Set(string cacheKey, object item, int minutes)
        {
            if (item != null)
            {
                if (minutes <= 0) minutes = 30;
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(minutes));
            }
        }
        public void Dispose(string cacheKey)
        {
            foreach (var element in MemoryCache.Default)
            {
                if(element.Key.Contains(cacheKey))
                    MemoryCache.Default.Remove(element.Key);
            }
        }
    }
}
