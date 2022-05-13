using System;
using System.Configuration;
using System.Runtime.Caching;

namespace CMS.Admin.Common
{
    public enum KhuyenMaiCachePriority
    {
        Default,
        NotRemovable
    }

    public class KhuyenMaiCache
    {
        // Gets a reference to the default MemoryCache instance. 
        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;
        public const string LOGTYPEND_LIST = "LOGTYPEND_LIST";

        public void AddToMyCache(String cacheKeyName, Object cacheItem, KhuyenMaiCachePriority myCacheItemPriority, int second = 0)
        {
            if (second == 0)
            {
                if (!int.TryParse(ConfigurationManager.AppSettings["CacheDay"], out second))
                    second = 864000;
            }

            callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (myCacheItemPriority == KhuyenMaiCachePriority.Default)
                                            ? CacheItemPriority.Default
                                            : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(second),
                RemovedCallback = callback
            };
            //policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePath));

            // Add inside cache 
            cache.Set(cacheKeyName, cacheItem, policy);
        }

        public Object GetMyCachedItem(String cacheKeyName)
        {
            // 
            return cache[cacheKeyName] as Object;
        }

        public static void RemoveAllCache()
        {
            RemoveMyCachedItem(LOGTYPEND_LIST);

            //MemoryCache.Default.Dispose();
        }
        public static void RemoveMyCachedItem(String cacheKeyName)
        {
            if (cache.Contains(cacheKeyName))
            {
                cache.Remove(cacheKeyName);
            }
        }

        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            String strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), "| Key-Name: ", arguments.CacheItem.Key, " | Value-Object: ",
            arguments.CacheItem.Value.ToString());
        }
    }
}