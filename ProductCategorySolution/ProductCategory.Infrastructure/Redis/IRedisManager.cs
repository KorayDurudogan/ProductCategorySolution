using System;

namespace ProductCategory.Infrastructure.Redis
{
    /// <summary>
    /// Interface for working with Redis cache.
    /// </summary>
    public interface IRedisManager
    {
        void Connect();

        void Add(string key, object value, TimeSpan timeSpan);

        T Get<T>(string key);
    }
}
