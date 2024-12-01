using OnlineStore.Contracts.Products;

namespace OnlineStore.ApiClient
{
    /// <summary>
    /// Интерфейс Api-клиента.
    /// </summary>
    public interface IOnlineStoreApiClient
    {
        /// <summary>
        /// Добавляет товар.
        /// </summary>
        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation);
    }
}
