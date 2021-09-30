using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace ProductCategory.Infrastructure.Redis
{
    public class RedisManager : IRedisManager
    {
        ConnectionMultiplexer connectionMultiplexer;

        public void Add(string key, object value, TimeSpan expireDate)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null || expireDate == default(TimeSpan))
                throw new Exception("Could not add value to Redis");

            if (Db.KeyExists(key))
                Db.KeyDelete(key);

            Db.StringSet(key, JsonConvert.SerializeObject(value), expireDate);
        }

        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");

        public T Get<T>(string key)
        {
            string jsonValue = Db.StringGet(key);
            return jsonValue == null ? default(T) : JsonConvert.DeserializeObject<T>(jsonValue);
        }

        private IDatabase Db { get => connectionMultiplexer.GetDatabase(1); }
    }
}
