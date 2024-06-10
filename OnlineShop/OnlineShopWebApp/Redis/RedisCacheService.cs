using StackExchange.Redis;

namespace OnlineShopWebApp.Redis
{
    public class RedisCacheService
    {
        private readonly IConnectionMultiplexer redis;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            this.redis = redis;
        }

        public async Task SetAsync(string key, string value)
        {
            var db = redis.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<string> GetAsync(string key)
        {
            var db = redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            var db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}