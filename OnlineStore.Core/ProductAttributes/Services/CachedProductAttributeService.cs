using OnlineStore.Contracts.ProductAttributes;
using OnlineStore.Core.Common.Cache;
namespace OnlineStore.Core.ProductAttributes.Services
{
    /// <summary>
    /// Кэширующий декоратор для сервиса атрибутов товаров.
    /// </summary>
    public sealed class CachedProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeService _productAtributeService;
        private readonly ICacheService _cacheService;

        /// <inheritdoc/>
        public CachedProductAttributeService(
            IProductAttributeService productAtributeService,
            ICacheService cacheService)
        {
            _productAtributeService = productAtributeService;
            _cacheService = cacheService;
        }

        /// <inheritdoc/>
        public Task<ProductAttributeDto> GetAsync(int id)
        {
            return _cacheService.GetOrSetAsync(
                key: $"ProductAttributes_{id}",
                lifeTime: TimeSpan.FromMinutes(10),
                func: async () => (await _productAtributeService.GetAsync(id)),
                cancellation: CancellationToken.None);
        }
    }
}
