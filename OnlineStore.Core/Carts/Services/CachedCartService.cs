using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.Carts;
using OnlineStore.Core.Carts.Helpers;
using OnlineStore.Core.Common.Cache;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Carts.Services
{
    /// <summary>
    /// Кэширующий сервис по работе с корзиной.
    /// </summary>
    public sealed class CachedCartService : ICartService
    {
        private readonly ICacheService _cacheService;
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <inheritdoc/>
        public CachedCartService(ICacheService cacheService,
            ICartService cartService,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _cacheService = cacheService;
            _cartService = cartService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public async Task AddProductToCartAsync(int productId, CancellationToken cancellation)
        {
            await _cartService.AddProductToCartAsync(productId, cancellation);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return;
            }

            await _cacheService.RemoveAsync(CartRedisKeyHelper.GetCartItemsCountKey(user.Id), cancellation);
        }

        /// <inheritdoc/>
        public Task<CartDto> GetCartAsync(CancellationToken cancellation)
        {
            return _cartService.GetCartAsync(cancellation);
        }

        /// <inheritdoc/>
        public async Task<int?> GetCartItemCountAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return null;
            }

            return await _cacheService.GetOrSetAsync(
                key: CartRedisKeyHelper.GetCartItemsCountKey(user.Id),
                lifeTime: TimeSpan.FromMinutes(60),
                func: async () => await _cartService.GetCartItemCountAsync(cancellation),
                cancellation: cancellation);
        }

        /// <inheritdoc/>
        public Task<bool> UpdateItemCountAsync(int productId, int newQuantity, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task RemoveItemAsync(int productId, CancellationToken cancellation)
        {
            await _cartService.RemoveItemAsync(productId, cancellation);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return;
            }

            await _cacheService.RemoveAsync(CartRedisKeyHelper.GetCartItemsCountKey(user.Id), cancellation);
        }

        public Task CheckoutAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
