using DataDeduplicationAPI.Contacts;
using StackExchange.Redis;

namespace DataDeduplicationAPI.Services
{
    public class DeduplicationService : IDeduplicationService
    {
        private readonly IDatabase _redisDb;

        public DeduplicationService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        // Check if the key exists in Redis (indicating a duplicate)
        public async Task<bool> IsDuplicateAsync(string key)
        {
            return await _redisDb.KeyExistsAsync(key);
        }

        // Add the key and data to Redis with an expiration time (e.g., 10 minutes)
        public async Task AddDataAsync(string key, string data)
        {
            await _redisDb.StringSetAsync(key, data, TimeSpan.FromMinutes(10));
        }
    }
}
