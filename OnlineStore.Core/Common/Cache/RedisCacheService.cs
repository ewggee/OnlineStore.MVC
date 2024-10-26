using OnlineStore.Core.Common.Redis;

namespace OnlineStore.Core.Common.Cache
{
    /// <summary>
    /// Сервис кэширования Редис.
    /// </summary>
    public sealed class RedisCacheService : ICacheService
    {
        private readonly IRedisCache _redisCacheService;

        /// <inheritdoc/>
        public RedisCacheService(IRedisCache redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        /// <inheritdoc/>
        public async Task<T> GetOrSetAsync<T>(string key, TimeSpan lifeTime, Func<Task<T>> func, CancellationToken cancellation)
        {
            var cachedItem = await _redisCacheService.GetAsync<T>(key, cancellation);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            var item = await func();

            if (item != null)
            {
                await _redisCacheService.SetAsync(key, item, lifeTime, cancellation);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(string key, CancellationToken cancelattion)
        {
            await _redisCacheService.RemoveAsync(key, cancelattion);
        }
    }
}
