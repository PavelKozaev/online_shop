using StackExchange.Redis;

namespace OnlineShopWebApp.Redis
{
    public class RedisCacheService
    {
        private readonly IConnectionMultiplexer redis;
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            this.redis = redis;
        }

        public async Task SetAsync(string key, string value)
        {
            await mutex.WaitAsync();
            try
            {
                var db = redis.GetDatabase();
                await db.StringSetAsync(key, value);
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mutex.Release();
            }
           
        }

        public async Task<string> GetAsync(string key)
        {
            try
            {
                var db = redis.GetDatabase();
                return await db.StringGetAsync(key);
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                var db = redis.GetDatabase();
                await db.KeyDeleteAsync(key);
            }
            catch (RedisConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }
    }
}