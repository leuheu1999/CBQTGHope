namespace Module.Framework.UltimateClient
{
    public interface ICacheService
    {
        T Get<T>(string cacheKey) where T : class;
        void Set(string cacheKey, object item, int minutes);
        void Dispose(string cacheKey);
    }
}
