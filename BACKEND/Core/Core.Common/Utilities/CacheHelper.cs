using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web.Configuration;
using System.Configuration;

namespace Core.Common.Utilities
{
    public static class CacheHelper
    {
        #region Caching

        private static readonly double ExpireMinutes = double.Parse(ConfigurationManager.AppSettings["CacheTimer"] ?? "15");
        public static T GetData<T>(string cacheKey) where T : class
        {
            var cacheItem = MemoryCache.Default[cacheKey] as T;
            if (cacheItem != null)
                return cacheItem;
            return null;
        }
        public static int GetDataInt(string cacheKey) 
        {
            var cacheItem = MemoryCache.Default[cacheKey] as string;
            int temp = 0;
            int.TryParse(cacheItem, out temp);
            return temp;
        }

        public static void SetData(string cacheKey, object data, double expireMinutes = 0)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(expireMinutes <= 0 ? ExpireMinutes : expireMinutes);
            MemoryCache.Default.Set(cacheKey, data, cacheItemPolicy);
        }

        public static void Remove(string cacheKey)
        {
            if (MemoryCache.Default.Any(x => x.Key.ToLower().Contains(cacheKey.ToLower())))
            {
                var lstCaches = MemoryCache.Default.Where(x => x.Key.ToLower().Contains(cacheKey.ToLower())).ToList();
                for (int i = 0; i < lstCaches.Count; i++)
                    MemoryCache.Default.Remove(lstCaches[i].Key);
            }
        }
        public static void RemoveAll()
        {
            var lstCaches = MemoryCache.Default.Select(x => new cache() { CacheKey = x.Key, Name = x.Key.Split('|')[0], CacheSize = GetObjectSize(x) }).ToList();

            for (int i = 0; i < lstCaches.Count; i++)
                MemoryCache.Default.Remove(lstCaches[i].CacheKey);
        }

        public static List<cache> GetAllCaches()
        {
            return MemoryCache.Default.Select(x => new cache() { CacheKey = x.Key, Name = x.Key.Split('|')[0], CacheSize = GetObjectSize(x) }).ToList();
        }
        #endregion

        #region BuildKey
        static public string BuildKey(string tableName, string parram)
        {
            if (tableName != string.Empty)
                return tableName.ToUpper() + (!string.IsNullOrEmpty(parram) ? "|" + parram : "");
            else
                return string.Empty;
        }
        #endregion

        private static int GetObjectSize(KeyValuePair<string, object> TestObject)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer bf = new System.Xml.Serialization.XmlSerializer(TestObject.Value.GetType());
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, TestObject.Value);

                var array = ms.ToArray();
                ms.Dispose();
                return array.Length;
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
    public class cache
    {
        private string name = "";
        private string cacheKey = "";
        private int cacheSize = 0;

        public int CacheSize
        {
            get { return cacheSize; }
            set { cacheSize = value; }
        }
        public string CacheKey
        {
            get { return cacheKey; }
            set { cacheKey = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
