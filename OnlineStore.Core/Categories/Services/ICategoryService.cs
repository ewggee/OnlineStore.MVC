using OnlineStore.Contracts.Categories;

namespace OnlineStore.Core.Categories.Services
{
    /// <summary>
    /// Сервис по работе с категориями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает список главных категорий.
        /// </summary>
        Task<List<CategoryDto>> GetMainCategoriesAsync(CancellationToken cancellation);

        /// <summary>
        /// Возвращает категорию по ID.
        /// </summary>
        Task<CategoryDto> GetAsync(int categoryId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает список подкатегорий в категории.
        /// </summary>
        Task<List<CategoryDto>> GetSubcategoriesByIdAsync(int categoryId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает список навигационных категорий (не содержат в себе товаров).
        /// <br>Исключает категорию по ID, которую нужно исключить из выборки, например, при обновлении данных (выбор родительской категории).</br>
        /// </summary>
        /// <param name="categoryId">ID категории, которую нужно исключить из выборки.</param>
        Task<List<CategoryDto>> GetNavigationCategoriesAsync(CancellationToken cancellation, int? categoryId = null);

        /// <summary>
        /// Возвращает категории, не содержащие подкатегории.
        /// </summary>
        Task<List<CategoryDto>> GetWithoutSubcategories(CancellationToken cancellation);

        /// <summary>
        /// Добавляет категорию в БД.
        /// </summary>
        Task AddAsync(CategoryDto categoryDto, CancellationToken cancellation);

        /// <summary>
        /// Обновляет значения категории.
        /// </summary>
        Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellation);

        // <summary>
        /// Удаляет категорию.
        /// </summary>
        Task DeleteAsync(int categoryId, CancellationToken cancellation);
    }
}
