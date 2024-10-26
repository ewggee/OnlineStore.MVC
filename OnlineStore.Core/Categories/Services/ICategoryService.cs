using OnlineStore.Contracts.Categories;

namespace OnlineStore.Core.Categories.Services
{
    /// <summary>
    /// Сервис по работе с категориями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Получает список категорий только для чтения.
        /// </summary>
        Task<IReadOnlyCollection<CategoryDto>> GetCategoriesAsync(CancellationToken cancellation);
    }
}
