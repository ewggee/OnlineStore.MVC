using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.Carts;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Carts.Repositories;
using OnlineStore.Core.Common.DateTimeProviders;
using OnlineStore.Core.Common.Extensions;
using OnlineStore.Core.Orders.Services;
using OnlineStore.Core.Products.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Carts.Services
{
    /// <summary>
    /// Сервис по работе с корзиной и заказом.
    /// </summary>
    public sealed class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartService(ICartRepository cartRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeProvider dateTimeProvider,
            IProductService productService,
            IOrderService orderService)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeProvider = dateTimeProvider;
            _productService = productService;
            _orderService = orderService;
        }

        /// <inheritdoc/>
        public async Task<CartDto> GetCartAsync(CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation);

            if (cart == null)
            {
                return new CartDto
                {
                    Items = [],
                    TotalPrice = 0
                };
            }

            return await GetCartItemsAsync(cart, cancellation);
        }

        /// <inheritdoc/>
        public async Task<int?> GetCartItemCountAsync(CancellationToken cancellation)
        {
            var existingCart = await GetCurrentUserCartAsync(cancellation);

            return existingCart?.Products?.Sum(x => x.Quantity) ?? 0;
        }

        /// <inheritdoc/>
        public async Task AddProductToCartAsync(int productId, CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingCart = await _cartRepository.GetCartByUserAsync(user.Id, cancellation);
            if (existingCart == null)
            {
                existingCart = new Cart
                {
                    UserId = user.Id,
                    Created = _dateTimeProvider.UtcNow,
                    StatusId = (int)CartStatusEnum.New
                };

                AddProductToCart(existingCart, productId);

                await _cartRepository.AddAsync(existingCart, cancellation);
            }
            else
            {
                existingCart.Updated = _dateTimeProvider.UtcNow;
                AddProductToCart(existingCart, productId);
                await _cartRepository.UpdateAsync(existingCart, cancellation);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateItemCountAsync(int productId, int newQuantity, CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation);

            var product = await _productService.GetAsync(productId, cancellation);

            if (newQuantity > product.StockQuantity || newQuantity == 0)
            {
                return false;
            }

            var productInCart = cart!.Products.First(cip => cip.ProductId == productId);
            productInCart.Quantity = newQuantity;

            await _cartRepository.UpdateAsync(cart, cancellation);

            return true;
        }

        /// <inheritdoc/>
        public async Task RemoveItemAsync(int productId, CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation)
                ?? throw new InvalidOperationException("Корзина текущего пользователя не найдена");

            var productInCart = cart.Products.FirstOrDefault(cp => cp.ProductId == productId)
                ?? throw new InvalidOperationException("Товар в корзине текущего пользователя не найден");

            cart.Products.Remove(productInCart);

            await _cartRepository.UpdateAsync(cart, cancellation);
        }

        /// <inheritdoc/>
        public async Task CheckoutAsync(CancellationToken cancellation)
        {
            var cart = await GetCurrentUserCartAsync(cancellation);

            #region Изменение количества товаров, формирование позиций заказа.
            var productsIds = cart!.Products
                .Select(cp => cp.ProductId)
                .ToArray();

            var products = await _productService.GetProductsByIdsAsync(productsIds, cancellation);

            var orderItems = new List<OrderItemDto>(cart.Products.Count);
            var totalPrice = 0m;
            foreach (var productInCart in cart.Products)
            {
                var product = products.First(p => p.Id == productInCart.ProductId);
                product.StockQuantity -= productInCart.Quantity;

                var item = new OrderItemDto
                {
                    ProductId = productInCart.ProductId,
                    Quantity = productInCart.Quantity,
                    UnitPrice = product.Price
                };
                orderItems.Add(item);
                totalPrice += product.Price * productInCart.Quantity;
            }

            await _productService.UpdateProductsCountAsync(products, cancellation);
            #endregion

            var orderDto = new OrderDto
            {
                UserId = cart.UserId,
                Items = orderItems.ToArray(),
                OrderDate = _dateTimeProvider.UtcNow,
                StatusId = (int)OrdersStatusEnum.Processing,
                TotalPrice = totalPrice
            };

            await _orderService.CreateAsync(orderDto, cancellation);

            cart!.StatusId = (int)CartStatusEnum.Done;
            cart.Closed = _dateTimeProvider.UtcNow;
            await _cartRepository.UpdateAsync(cart, cancellation);
        }

        /// <summary>
        /// Добавляет новый товар в корзину или увеличивает количество уже имеющегося на единицу.
        /// </summary>
        /// <param name="cart">Корзина.</param>
        /// <param name="productId">ID товара.</param>
        private static void AddProductToCart(Cart cart, int productId)
        {
            var productInCart = cart.Products.FirstOrDefault(p => p.ProductId == productId);

            if (productInCart != null)
            {
                productInCart.Quantity++;
            }
            else
            {
                cart.Products.Add(new CartProduct
                {
                    Cart = cart,
                    Quantity = 1,
                    ProductId = productId
                });
            }
        }

        /// <summary>
        /// Возвращает корзину текущего пользователя.
        /// </summary>
        private async Task<Cart?> GetCurrentUserCartAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                return null;
            }

            return await _cartRepository.GetCartByUserAsync(user.Id, cancellation);
        }

        /// <summary>
        /// Возвращает актуальные данные содержимого корзины.
        /// </summary>
        private async Task<CartDto> GetCartItemsAsync(Cart cart, CancellationToken cancellation)
        {
            var productsIds = cart.Products
                .Select(p => p.ProductId)
                .ToArray();

            var products = await _productService.GetProductsByIdsAsync(productsIds, cancellation);

            var cartItems = new List<CartItemDto>(products.Count);
            var totalPrice = 0m;
            foreach (var productInCart in cart.Products)
            {
                var product = products.First(p => p.Id == productInCart.ProductId);
                cartItems.Add(new CartItemDto
                {
                    Price = product.Price,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = productInCart.Quantity
                });
                totalPrice += product.Price * productInCart.Quantity;
            }

            return new CartDto
            {
                Items = cartItems,
                TotalPrice = Math.Round(totalPrice, 2)
            };
        }
    }
}
