using OnlineStore.Contracts.Carts;

namespace OnlineStore.Core.Carts.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с корзиной.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Добавляет товар по его ID в корзину текущего пользователя.
        /// </summary>
        Task AddProductToCartAsync(int productId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает количество товаров в корзине текущего пользователя.
        /// </summary>
        Task<int?> GetCartItemCountAsync(CancellationToken cancellation);

        /// <summary>
        /// Получает содержимое корзины товаров текущего пользователя.
        /// </summary>
        Task<CartDto> GetCartAsync(CancellationToken cancellation);

        /// <summary>
        /// Удаляет товар по ID из корзины.
        /// </summary>
        Task RemoveItemAsync(int productId, CancellationToken cancellation);
    }
}
