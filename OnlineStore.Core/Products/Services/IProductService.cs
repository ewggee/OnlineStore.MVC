using OnlineStore.Contracts.Categories;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Products;

namespace OnlineStore.Core.Products.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с товарами.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Возвращает информацию о продукте по его ID.
        /// </summary>
        /// <param name="productId">Идентификатор продукта.</param>
        Task<ShortProductDto> GetProductByIdAsync(int productId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает пагинированный список товаров в категории.
        /// </summary>
        /// <param name="request">Запрос на получение списка товаров.</param>
        /// <param name="categoryId">ID категории.</param>
        Task<ProductsListDto> GetProductsInCategoryByRequestAsync(PagedRequest request, CategoryDto categoryDto, CancellationToken cancellation);

        /// <summary>
        /// Возвращает список товаров по ID категории.
        /// </summary>
        Task<List<ShortProductDto>> GetAllProductsInCategoryByIdAsync(int categoryId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает список товаров по их ID.
        /// </summary>
        /// <param name="productsIds">Список ID товаров.</param>
        Task<List<ShortProductDto>> GetProductsByIdsAsync(List<int> productsIds, CancellationToken cancellation);

        /// <summary>
        /// Добавляет товар.
        /// </summary>
        /// <param name="productDto">Транспортная модель товара.</param>
        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation);

        /// <summary>
        /// Обновляет данные о товаре.
        /// </summary>
        Task UpdateAsync(ShortProductDto shortProductDto, CancellationToken cancellation);

        /// <summary>
        /// Удаляет товар по ID.
        /// </summary>
        Task DeleteAsync(int productId, CancellationToken cancellation);
    }
}
