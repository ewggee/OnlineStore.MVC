using OnlineStore.Core.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Categories.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Возвращает все главные категории.
        /// </summary>
        Task<List<Category>> GetMainAsync(CancellationToken cancellation);

        /// <summary>
        /// Возвращает подкатегории по ID категории.
        /// </summary>
        Task<List<Category>> GetSubsAsync(int categoryId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает навигационные категории (которые не имеют товаров, служат для построения иерархии категорий).
        /// <br>Исключает категорию по ID, а также её подкатегории.</br>
        /// </summary>
        /// <param name="categoryId">ID категории, которую нужно исключить из выборки. Исключаются также её подкатегории.</param>
        Task<List<Category>> GetNavigationCategoriesAsync(CancellationToken cancellation, int? categoryId);

        /// <summary>
        /// Возвращает категории, которые не имеют подкатегорий.
        /// </summary>
        Task<List<Category>> GetWithoutSubs(CancellationToken cancellation);
    }
}
